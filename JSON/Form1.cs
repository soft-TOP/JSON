using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml;
using Newtonsoft.Json;

namespace JSON
{
    public partial class Form1 : Form
    {
        private String line = Environment.NewLine + String.Empty.PadLeft(30, '-') + Environment.NewLine;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(Object sender, EventArgs e)
        {
            //eine Person serialisieren
            using (MemoryStream stream1 = new MemoryStream())
            {

                Person p = new Person();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(p.GetType());
                p.Name = "Marius";
                p.Alter = 37;
                serializer.WriteObject(stream1, p);

                stream1.Position = 0;
                StreamReader sr = new StreamReader(stream1);
                textBox1.Text = "JSON object eine Person: " + line + sr.ReadToEnd() + line;

                //eine Person deserialisieren
                stream1.Position = 0;
                p = (Person)serializer.ReadObject(stream1);
                textBox1.Text += $"eine Person Deserialized, zurück={Environment.NewLine}{nameof(p.Name)}: {p.Name}; {nameof(p.Alter)}: {p.Alter}{line}";
            }

            //mehrere Personen serialisieren
            using (MemoryStream stream2 = new MemoryStream())
            {
                List<Person> pList = new List<Person>
                {
                new Person() {Name = "Klaus", Alter = 52 },
                new Person() {Name = "Heidi", Alter = 22 },
                new Person() {Name = "Martha", Alter = 98 }
                };
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(pList.GetType());
                serializer.WriteObject(stream2, pList);

                stream2.Position = 0;
                StreamReader sr = new StreamReader(stream2);
                textBox1.Text += "JSON object mehrere Personen: " + line + sr.ReadToEnd() + line;

                stream2.Position = 0;
                pList = null; // nur um sicherzugehen, dass nichts übrig ist
                pList = (List<Person>)serializer.ReadObject(stream2);
                textBox1.Text += $"mehrere Personen Deserialized, zurück={Environment.NewLine}";
                foreach (var p in pList)
                {
                    textBox1.Text += $"{ nameof(p.Name)}: { p.Name}; { nameof(p.Alter)}: { p.Alter}{Environment.NewLine}";
                }
            }

            //eine Person mit Hilfe JSONHelper serialisieren
            Person p2 = new Person();
            p2.Name = "Constantin";
            p2.Alter = 75;
            String jsonString = JsonHelper.JsonSerializer(p2);
            textBox1.Text += line + $"JsonHelper: {jsonString}";

            p2 = JsonHelper.JsonDeserialize<Person>(jsonString);
            textBox1.Text += $"eine Person Deserialized, zurück={Environment.NewLine}{nameof(p2.Name)}: {p2.Name}; {nameof(p2.Alter)}: {p2.Alter}{line}";

            //mehrere Personen serialisieren
            List<Person> pList2 = new List<Person>
            {
            new Person() {Name = "Laura", Alter = 27 },
            new Person() {Name = "Carlos", Alter = 45 },
            new Person() {Name = "Manuela", Alter = 41 }
            };
            jsonString = JsonHelper.JsonSerializer(pList2);
            textBox1.Text += line + $"JsonHelper: {jsonString}{line}";

            pList2 = JsonHelper.JsonDeserialize<List<Person>>(jsonString);
            foreach (var pp in pList2)
            {
                textBox1.Text += $"{ nameof(pp.Name)}: { pp.Name}; { nameof(pp.Alter)}: { pp.Alter}{Environment.NewLine}";
            }


            // mehrere Personen mittels JsonTextReader deserialisieren
            // muss per paketmanager installiert werden!
            textBox1.Text += line + "JsonTextReader";
            JsonTextReader reader = new JsonTextReader(new StringReader(jsonString));
            while (reader.Read())
            {
                textBox1.Text += $"Token: {reader.TokenType}{((reader.Value == null) ? "" : $", Value: {reader.Value}")}{Environment.NewLine}";
            }
        }
    }
}


