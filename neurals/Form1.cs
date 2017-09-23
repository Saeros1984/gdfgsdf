using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace neurals
{
    public partial class Baseform : Form
    {
        static double randomweight(int min, int max)
        {
            return ((Double)(new Random().Next(min, max))/100);
        }
        static double normalize(double enter, double min=0, double max=100, double intmin=0, double intmax=1)//нормализация входного вектора
        {
            return (enter-min)*(intmax-intmin)/max-min+intmin;
        }
        public Baseform()
        {
            InitializeComponent();
        }
        class network
        {
            public string name;
            public List<layer> layers=new List<layer>();
            public List<neuron> neurons=new List<neuron>();
            public List<List<neuron>> enters = new List<List<neuron>>();//массив входов
            public List<List<neuron>> outs = new List<List<neuron>>();//массив выходов
            public double alpha = 1;//крутизна сигмоиды
            public string rawdata;//необработанная строка данных
            public List<List<List<object>>> data = new List<List<List<object>>>();//готовый к обучению массив с выборкой
            public List<List<string>> distinct;//используется для определения различных элементов в каждом столбце.
            public List<List<List<List<double>>>> normalizedData = new List<List<List<List<double>>>>();//массив нормализованных данных
            public ArrayList[] normalizationTable;

            public void readfromXML(string addr, int[] outss)
            {
                rawdata = System.IO.File.ReadAllText(@addr);
                Regex takerow = new Regex("<Row(.|\n)*?>(.|\n)*?</Row>");
                Regex takecell = new Regex("<Cell.*?>.*?<Data(.*?Type=\"(.*?)\"|.*?)>((.|\n)*?)</Data></Cell>");//2 - тип, 3 - данные
                distinct = new List<List<string>>();
                for (int i = takecell.Matches(takerow.Match(rawdata).Groups[0].ToString()).Count; i > 0; i--) distinct.Add(new List<string>());
                foreach (Match match in takerow.Matches(rawdata))
                {
                    data.Add(new List<List<object>>());
                    int p=0;
                    foreach (Match cell in takecell.Matches(match.Groups[0].ToString()))
                    {
                        data[data.Count - 1].Add(new List<object>());
                        if (!outss.Contains(data[data.Count - 1].IndexOf(data[data.Count - 1].Last()))) 
                            data[data.Count - 1][data[data.Count - 1].Count-1].Add("x-data");
                        else
                            data[data.Count - 1][data[data.Count - 1].Count - 1].Add("y-data");
                        int j;
                        if (Int32.TryParse(cell.Groups[3].ToString(), out j))
                        {
                            data[data.Count - 1][data[data.Count - 1].Count - 1].Add(j);
                            data[data.Count - 1][data[data.Count - 1].Count - 1].Add("numeric");
                            distinct[p].Add(j.ToString());
                            p++;
                        }
                        else
                        {
                            data[data.Count - 1][data[data.Count - 1].Count - 1].Add(cell.Groups[3].ToString());
                            data[data.Count - 1][data[data.Count - 1].Count - 1].Add("string");
                            distinct[p].Add(cell.Groups[3].ToString());
                            p++;
                        }

                    }
                    
                }
            }
            public List<List<string>> dist(List<List<string>> disti)
            {
                List<List<string>> llll = new List<List<string>>();
                foreach (List<string> oo in disti)
                {
                    llll.Add(oo.Distinct().ToList<string>());
                }
                return llll;
            }
            public void normalize(string type)
            {
                normalizedData.Add(new List<List<List<double>>>());
                normalizedData.Add(new List<List<List<double>>>());
                distinct = dist(distinct);
                foreach (List<object> coll in data[0])
                {
                    if (distinct[data[0].IndexOf(coll)].Distinct().Count()==2)
                    {
                        coll[2] = "bool";
                    }
                }
                normalizationTable=new ArrayList[distinct.Count];
                foreach(List<string> li in distinct)
                {
                    normalizationTable[distinct.IndexOf(li)] = new ArrayList();
                    if (data[0][distinct.IndexOf(li)][2]=="numeric")
                    {
                        normalizationTable[distinct.IndexOf(li)].Add("numeric");
                    }
                    else
                    {
                       if(data[0][distinct.IndexOf(li)][2]=="bool")
                       {
                           
                           foreach(string str in li)
                           {
                               ArrayList boolnorm = new ArrayList();
                               boolnorm.Add(str);
                               boolnorm.Add(new List<double>());
                               (boolnorm[1] as List<double>).Add(li.IndexOf(str));
                               normalizationTable[distinct.IndexOf(li)].Add(boolnorm);
                           }
                       }
                       if (data[0][distinct.IndexOf(li)][2] == "string")
                       {
                           foreach (string str in li)
                           {
                               ArrayList categnorm = new ArrayList();
                               categnorm.Add(str);
                               categnorm.Add(new List<double>());   
                               foreach(string hh in li)
                               {
                                   (categnorm[1] as List<double>).Add(0);
                               }
                               (categnorm[1] as List<double>)[li.IndexOf(str)] = 1;
                               normalizationTable[distinct.IndexOf(li)].Add(categnorm);
                           }
                       }
                    }
                }
                double min = 99999999;
                double max = -99999999;
                foreach (List<string> dis in distinct)
                {
                    if (data[0][distinct.IndexOf(dis)][2]=="numeric")
                    {
                        if (Int32.Parse(dis.Min().ToString()) < min) min = Int32.Parse(dis.Min().ToString());
                        if (Int32.Parse(dis.Max().ToString()) > max) max = Int32.Parse(dis.Max().ToString());
                    }
                }
                foreach (List<List<object>> row in data)
                {
                    normalizedData[0].Add(new List<List<double>>());
                    normalizedData[1].Add(new List<List<double>>());
                    foreach (List<object> coll in row)
                    {
                        if (data[0][row.IndexOf(coll)][2] == "numeric")
                        {
                            if (type == "linear")
                            {
                                if(coll[0]=="x-data")normalizedData[0].Last().Add(linearNorm(min, max, coll));
                                if (coll[0] == "y-data") normalizedData[1].Last().Add(linearNorm(min, max, coll));
                            }
                        }
                        if (row[0][2] == "bool")
                        {
                            coll[2] = "bool";
                            //if (coll[0] == "x-data") normalizedData[0].Last().Add(new List<double>().Add());
                        }
                    }
                }
            }
            public List<double> linearNorm(double min, double max, List<object> col)
            {
                if (col[2]!="numeric")
                {
                    MessageBox.Show("Нецифровые данные!");
                    return col[3] as List<double>;
                }

                if(col.Count<4)col.Add(new List<double>());
                List<double> oo= col[3] as List<double>;
                oo.Add((Int32.Parse(col[1].ToString()) - min) / (max - min));
                col[3] = oo as List<double>;
                return oo;
            }
            public layer lastLayer()
            {
                return layers[layers.Count - 1];
            }
            public layer prevLayer()
            {
                return layers[layers.Count - 2];
            }
            public void connectToPrev()
            {
                layers[layers.Count - 1].connectLayer(layers[layers.Count - 2]);
            }
            public layer addLayer(int neuronsnum = 0, string typ = "hidden")
            {
                layer lay = new layer(typ);
                for (int i = 0; i < neuronsnum; i++)
                {
                    neuron neu=lay.addNeuron();
                    if (enters.Count==0) enters.Add(new List<neuron>());
                    if (outs.Count == 0) enters.Add(new List<neuron>());
                    if (typ == "enter") enters[0].Add(neu);
                    if (typ == "out") outs[0].Add(neu);
                }
                layers.Add(lay);
                
                return lay;
            }
            public void enterGeneration()
            {
                enterOut();
            }
            public void outGeneration()
            {
                enterOut("out");
            }
            private void enterOut(string type="enter")
            {
                if (data.Count < 1 || distinct.Count < 1) { MessageBox.Show("Отсутствуют входные данные!"); return; }
                addLayer(0, type);
                foreach (List<string> li in distinct)
                {
                    if (data[0][distinct.IndexOf(li)][0].ToString() != "x-data"&&type=="enter") continue;
                    if (data[0][distinct.IndexOf(li)][0].ToString() != "y-data" && type == "out") continue;
                    if (type=="enter")enters.Add(new List<neuron>());else if (type=="out")outs.Add(new List<neuron>());
                    if (data[0][distinct.IndexOf(li)][2].ToString() == "numeric")
                    {
                        if (data[0][distinct.IndexOf(li)][0].ToString() == "x-data"&&type == "enter") enters.Last().Add(layers.Last().addNeuron());else if (data[0][distinct.IndexOf(li)][0].ToString() == "y-data" && type == "out") outs.Last().Add(layers.Last().addNeuron());
                    }
                    else if (data[0][distinct.IndexOf(li)][2].ToString() == "string")
                    {
                        for (int i = 0; i < li.Distinct().Count(); i++)
                        {
                            if (type == "enter") enters.Last().Add(layers[0].addNeuron()); else if (type == "out") outs.Last().Add(layers[0].addNeuron());
                        }
                    }
                    else if (false)
                    {
                        //здесь будет код для данных в виде слов
                    }
                }
            }
            public network(int layersnum=0, int enternum=0, int neuronsnum=0, int outnum=0)
            {
                if (layersnum == 0) return;
                layers.Add(addLayer(enternum, "enter"));
                enters[0].AddRange(layers.Last().neurons);
                for (int i = 0; i < layersnum; i++)
                {
                    layers.Add(addLayer(neuronsnum));
                    layers.Last<layer>().connectLayer(layers[layers.Count-1]);
                }
                layers.Add(addLayer(outnum, "out"));
                outs[0].AddRange(layers.Last().neurons);
            }
            public void getAnswer()
            {
                for (int i=0;i<layers.Count;i++)
                {
                    layers[i].getOuts();
                }
            }
            public void visual()
            {
                string res="";
                foreach (List<neuron> lis in outs)
                {
                    foreach (neuron ner in lis)
                    {
                        res += "O";
                    }
                    res += " ";
                }
                res += Environment.NewLine;
                foreach (layer lay in layers)
                {
                    if (lay.type == "enter" || lay.type == "out") continue;
                    foreach (neuron ner in lay.neurons)
                    {
                        res += "o";
                    }
                    res += Environment.NewLine;
                }
                foreach (List<neuron> lis in enters)
                {
                    foreach (neuron ner in lis)
                    {
                        res += "O";
                    }
                    res += " ";
                }
                MessageBox.Show(res);
            }
        }
        class layer
        {
            public List<neuron> neurons=new List<neuron>();
            public string type;
            public layer(string typ = "hidden")
            {
                type=typ;
            }
            public neuron addNeuron()
            {
                neuron neu=new neuron();
                neu.type = type;
                neurons.Add(neu);
                return neu;
            }
            public void connectLayer(layer lay, int min=1, int max=50)// получает слой, от которого идут сигналы
            {
                foreach (neuron neu in this.neurons)
                {
                    foreach (neuron ent in lay.neurons)
                    {
                        neu.sinapses.Add(ent, randomweight(min, max));
                        ent.receivers.Add(neu);
                    }
                }
            }
            public void getOuts() //собирает все выходы нейронов, подключенных к нейронам данного слоя
            {
                foreach (neuron neu in neurons)
                {
                    neu.takeNET();
                    neu.logistic(1);
                }
            }
        }
        class neuron
        {
            public string type;
            public double net=0;
            public double Out=0;
            public double a = 1;
            public Dictionary<neuron, double> sinapses = new Dictionary<neuron, double>();
            public List<neuron> receivers = new List<neuron>();
            public void createsinaps(neuron nn, double weight=0) //получает нейрон, от которого будет передаваться значение
            {
                sinapses.Add(nn, weight);
                nn.receivers.Add(this);
            }
            public void takeNET()
            {
                foreach (KeyValuePair<neuron, double> taker in sinapses)
                {
                    net+=taker.Key.Out*taker.Value;
                }
            }
            public void logistic(double alpha=1)
            {
                Out=1/(1+Math.Pow(Math.E, -net*alpha));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            network nnn = new network();
            nnn.readfromXML(@"C:\Users\User\Desktop\книга2.xml", new int[] {4});
            nnn.enterGeneration();
            nnn.addLayer(10);
            nnn.addLayer(10);
            nnn.addLayer(10);
            nnn.outGeneration();
            nnn.normalize("linear");
            string res="";
            foreach(List<List<double>>ppp in nnn.normalizedData[0])
            {
                foreach (List<double> uuu in ppp)
                {
                    foreach (double rrr in uuu) res += Math.Round(rrr, 3) + " ";
                }
                res += Environment.NewLine;
            }
            //MessageBox.Show(res);
            //nnn.visual();
            MessageBox.Show((nnn.normalizationTable[0][0] as ArrayList)[0].ToString());
        }
    }
}
