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
        static Random rand = new Random();
        public object retobj()
        {
            return label1;
        }
        static double randomweight(int min, int max)
        {
            return ((Double)(rand.Next(min, max))/100);
        }
        static double normalize(double enter, double min=0, double max=100, double intmin=0, double intmax=1)//нормализация входного вектора
        {
            return (enter-min)*(intmax-intmin)/max-min+intmin;
        }
        static void visuallist(IList lis, int level=1)//визуализатор списка
        {
            string res = "";
            foreach (object obj in lis)
            {
                res += obj.ToString();
                if (level>1)
                {
                    res += Environment.NewLine;
                    foreach(object obj2 in (obj as IList))
                    {
                        res += obj2.ToString()+"  ";
                    }
                }
                else
                {
                    res += "  ";
                }
            }
            MessageBox.Show(res);
        }
        static void visualout(network nn)//визуализатор списка
        {
            string res = "";
            foreach (neuron obj in nn.oute.neurons)
            {
                res += obj.Out.ToString();
                    res += Environment.NewLine;

            }
            MessageBox.Show(res);
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
            public double constant=-1;//значение постоянных нейронов, ноль отключает их вообще
            public layer oute = new layer();
            public layer inn = new layer();
            public List<double> comparer = new List<double>();
            public datarow currentrow;//текущая строка данных
            public double alpha = 1;//крутизна сигмоиды
            public string rawdata;//необработанная строка данных
            public double speed = 0.1;//скорость обучения
            //public List<double> mistake;//сюда будем собирать ошибку
            //public List<List<List<object>>> data = new List<List<List<object>>>();//готовый к обучению массив с выборкой
            
            //public List<List<List<List<double>>>> normalizedData = new List<List<List<List<double>>>>();//массив нормализованных данных
            //public ArrayList[] normalizationTable;
            public data data = new data();
            public void readfromXML(string addr, int[] outss)
            {
                rawdata = System.IO.File.ReadAllText(@addr);
                Regex takerow = new Regex("<Row(.|\n)*?>(.|\n)*?</Row>");
                Regex takecell = new Regex("<Cell.*?>.*?<Data(.*?Type=\"(.*?)\"|.*?)>((.|\n)*?)</Data></Cell>");//2 - тип, 3 - данные
                data.distinct = new List<List<string>>();
                
                //for (int i = takecell.Matches(takerow.Match(rawdata).Groups[0].ToString()).Count; i > 0; i--) data.distinct.Add(new List<string>());
                //формируем параметры колонок
                foreach (Match match in takecell.Matches(takerow.Matches(rawdata)[0].Groups[0].ToString()))
                {
                    data.distinct.Add(new List<string>());
                    data.Columns.Add(new string[2]);
                    if (!outss.Contains(1+data.Columns.IndexOf(data.Columns.Last()))) 
                        data.Columns.Last()[1]="x-data";
                    else
                        data.Columns.Last()[1]="y-data";
                    int j;
                    if (Int32.TryParse(match.Groups[3].ToString(), out j))
                    {
                        data.Columns.Last()[0]="numeric";
                    }
                    else
                    {
                        data.Columns.Last()[0] = "categ";
                    }
                }
                foreach (Match match in takerow.Matches(rawdata))
                {
                    data.addRow();
                    foreach (Match cell in takecell.Matches(match.Groups[0].ToString()))
                    {
                        datacell cel=data.lastRow().addCell();
                        cel.data = cell.Groups[3].ToString(); ;
                        data.distinct[cel.columnNumber - 1].Add(cel.data);

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
            /*public void normalize(string type)
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
                        if (data[0][row.IndexOf(coll)][2] == "bool")
                        {
                            coll[2] = "bool";
                            double boolval;
                            if (normalizationTable[row.IndexOf(coll)][0][0] == coll[1].ToString()) boolval = 1; else boolval = 0;
                            if (coll[0] == "x-data")
                            {
                                
                                normalizedData[0].Last().Add(new List<double>());
                                normalizedData[0].Last().Last().Add(boolval);
                            }
                            else
                            {
                                normalizedData[1].Last().Add(new List<double>());
                                normalizedData[1].Last().Last().Add(boolval);
                            }
                        }
                        if (data[0][row.IndexOf(coll)][2] == "string")
                        {
                            foreach (string itog in normalizationTable[row.IndexOf(coll)])
                            {
                                if (coll[1].ToString()==itog)
                                {

                                }
                            }
                        }
                    }
                }
            }*/
            public void totalgen()//подготовка данных для обучения
            {
                neurons.Clear();
                layers.Clear();
                outs.Clear();
                oute.neurons.Clear();
                inn.neurons.Clear();
                enters.Clear();
                data.minmaxes();
                data.normtable.formdistinct();
                data.normtable.generateNorm();
                data.generateNorm();
                enterOut();
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
                    neurons.Add(lay.addNeuron());
                    
                }
                layers.Add(lay);
                
                return lay;
            }
            public void enterOut()
            {
                if (data.datarows.Count < 1 || data.distinct.Count < 1) { MessageBox.Show("Отсутствуют входные данные!"); return; }
                oute.type = "out";
                inn.type = "enter";
                for (int i = 0; i < data.Columns.Count;i++)
                {
                    if(data.Columns[i][1]=="x-data")
                    {
                        enters.Add(new List<neuron>());
                        foreach (double dd in data.normtable.normtable[i][0].norm)
                        {
                            enters.Last().Add(inn.addNeuron());
                        }
                        neurons.AddRange(enters.Last());
                    }
                    if (data.Columns[i][1] == "y-data")
                    {
                        outs.Add(new List<neuron>());
                        foreach (double dd in data.normtable.normtable[i][0].norm)
                        {
                            outs.Last().Add(oute.addNeuron());
                            comparer.Add(0);
                        }
                        neurons.AddRange(outs.Last());
                    }

                }
            }
            public void addEnter()
            {
                layers.Add(inn);
            }
            public void addOut()
            {
                layers.Add(oute);
            }
            public network(int layersnum=0, int enternum=0, int neuronsnum=0, int outnum=0)
            {
                if (layersnum == 0) return;
                layers.Add(inn);
                for (int i = 0; i < layersnum; i++)
                {
                    layers.Add(addLayer(neuronsnum));
                    layers.Last<layer>().connectLayer(layers[layers.Count-1]);
                }
                layers.Add(oute);
            }
            public void setEnter(int rownum)
            {
                int neurocount=0;
                currentrow = data.datarows[rownum];
                foreach (datacell dat in data.datarows[rownum].datacells)
                {
                    if (data.Columns[dat.columnNumber-1][1]=="x-data")
                    foreach (double dd in dat.normalized)
                    {
                        inn.neurons[neurocount].Out = dd;
                        neurocount++;
                    }
                }
            }
            public void getAnswer()
            {
                for (int i=1;i<layers.Count;i++)
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
            public void backPrograd()
            {
                for (int i=90000;i>0;i--)
                {
                    foreach (datarow row in data.datarows)
                    {
                        getOutMistake(row);
                        totalvisual("ПОЛУЧЕНИЕ ОШИБКИ");
                        mistakeToEnter();
                        totalvisual("РАСПРОСТРАНЕНИЕ ОШИБКИ ВНИЗ");
                        weightCorrect();
                        totalvisual("РЕЗУЛЬТАТЫ КОРРЕКТИРОВКИ ВЕСОВ");
                    }
                }
            }
            public void getOutMistake(datarow row)
            {
                    setEnter(data.datarows.IndexOf(row));
                    getAnswer();
                    totalvisual("ПЕРВИЧНЫЙ РЕЗУЛЬТАТ ПРОХОДА");
                    for (int i = 0; i < oute.neurons.Count;i++ )
                    {
                        //MessageBox.Show(row.outVector[i].ToString()+Environment.NewLine+oute.neurons[i].Out.ToString());
                        //MessageBox.Show(layers[0].neurons[0].Out.ToString() + Environment.NewLine + layers[0].neurons[1].Out.ToString());
                        oute.neurons[i].q = oute.neurons[i].Out * (1 - oute.neurons[i].Out) * (row.outVector[i] - oute.neurons[i].Out);
                    }           
            }
            public void mistakeToEnter()
            {
                for (int i=layers.Count-2;i>=0;i--)
                {
                    foreach(neuron nn in layers[i].neurons)
                    {
                        nn.takeMistake();
                    }
                }
            }
            public void weightCorrect()
            {
                for(int i=layers.Count-1;i>0;i--)
                {
                    foreach (neuron nn in layers[i].neurons)
                    {
                        Dictionary<neuron, double> uuu= new Dictionary<neuron,double>(nn.sinapses);
                        foreach (KeyValuePair<neuron, double> val in nn.sinapses)
                        {
                            uuu[val.Key] = val.Value + nn.q * speed * val.Key.Out;
                        }
                        nn.sinapses=new Dictionary<neuron,double>(uuu);
                    }

                }
            }
            public void totalvisual(string mes="", bool act=false)
            {
                if (!act) return;
                string res = "Требуемый выход: ";
                int round = 4;
                foreach (double dd in currentrow.outVector)
                {
                    res += dd.ToString() + "  ";
                }
                res += Environment.NewLine + Environment.NewLine;
                for(int lay=layers.Count-1;lay>=0;lay--)
                {
                    string layer = Environment.NewLine;
                    string weight = Environment.NewLine;
                    for (int neu = 0; neu <= layers[lay].neurons.Count - 1; neu++)
                    {
                        layer += "       (net=" + Math.Round(layers[lay].neurons[neu].net, round).ToString() + "; OUT=" + Math.Round(layers[lay].neurons[neu].Out, round).ToString() + "; q=" + Math.Round(layers[lay].neurons[neu].q, round).ToString() + ")";
                        for (int we = 0; we <= layers[lay].neurons[neu].sinapses.Count - 1; we++)
                        {
                            weight += "  "+Math.Round(layers[lay].neurons[neu].sinapses.Values.ToArray()[we], round).ToString();
                        }
                        weight += "    ";
                    }
                    res += layer;
                    res += weight;
                }
                MessageBox.Show(res + Environment.NewLine + mes);
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
            public neuron addNeuron(string typ="", double ou=0)
            {
                neuron neu=new neuron();
                if (typ=="")
                neu.type = type;
                else
                neu.type = typ;
                neu.Out = ou;
                neurons.Add(neu);
                return neu;
            }
            public void connectLayer(layer lay, int min=-60, int max=60)// получает слой, от которого идут сигналы
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
            public void visual()
            {
                string res = "";
                foreach (neuron ne in neurons)
                {
                    res += ne.Out.ToString()+"     ";
                }
                MessageBox.Show(res);
            }
        }
        class neuron
        {
            public string type;
            public double net=0;
            public double Out=0;
            public double q=0;
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
                if(type=="const")return;
                net = 0;
                foreach (KeyValuePair<neuron, double> taker in sinapses)
                {
                    net+=taker.Key.Out*taker.Value;
                }
            }
            public void takeMistake()
            {
                q=0;
                foreach (neuron nn in receivers)
                {
                    q += nn.sinapses[this]*nn.q;
                    //MessageBox.Show(nn.sinapses[this].ToString() + Environment.NewLine + nn.q.ToString());
                }
                q = q * Out * (1 - Out);
            }
            public void logistic(double alpha=1)
            {
                if (type == "const") return;
                Out=1/(1+Math.Pow(Math.E, -net*alpha));
            }
        }
        class data
        {
            public List<datarow> datarows = new List<datarow>();
            public List<string[]> Columns = new List<string[]>();//1 - тип, 2 - роль в выборке
            public List<List<string>> distinct;//используется для определения различных элементов в каждом столбце.
            public double min=9999999999;
            public double max=-9999999999;
            public normalizationTable normtable;
            public void minmaxes()
            {
                for (int i=0;i<Columns.Count;i++)
                {
                    double ii; 
                    if (Columns[i][0]=="numeric")
                     {
                         foreach (string str in distinct[i])
                         {
                             ii=Int32.Parse(str);
                             if (ii > max)
                                 max = ii;
                             if (ii < min)
                                 min = ii;
                         }
                     }
                }
            }
            public List<double> linearNorm(string enter)
            {
                List<double> oo = new List<double>();
                oo.Add((Int32.Parse(enter) - min) / (max - min));
                return oo;
            }
            public datarow addRow()
            {
                datarow row = new datarow(this);
                datarows.Add(row);
                return row;
            }
            public datarow lastRow()
            {
                return datarows.Last();
            }
            public void generateNorm()
            {
                foreach (datarow row in datarows)
                {
                    foreach (datacell cel in row.datacells)
                    {
                        foreach (normcell cell in normtable.normtable[cel.columnNumber-1])
                        {
                            if (cell.raw == cel.data)
                                cel.normalized = cell.norm;
                        }
                    }
                    row.generateOutVector();
                }
            }
            public data()
            {
                normtable = new normalizationTable(this);
            }
        }
        class datarow
        {
            public List<datacell> datacells = new List<datacell>();
            public List<double> outVector = new List<double>();
            public data motherData;
            public datacell addCell()
            {
                datacell cell = new datacell(this);
                datacells.Add(cell);
                return cell;
            }
            public datacell lastCell()
            {
                return datacells.Last();
            }
            public void generateOutVector()
            {
                foreach (datacell cel in datacells)
                {
                    if(motherData.Columns[cel.columnNumber-1][1]=="y-data")
                    foreach(double dd in cel.normalized)
                    {
                        outVector.Add(dd);
                    }
                }
            }
            public datarow(data mdata)
            {
                motherData = mdata;
            }
        }
        class datacell
        {
            public string data;
            public List<double> normalized = new List<double>();
            public int columnNumber;
            public datarow motherRow;
            public datacell(datarow mRow)
            {
                motherRow = mRow;
                columnNumber=motherRow.datacells.Count+1;
            }
        }
        class normalizationTable
        {
            private data motherdata;
            public List<List<normcell>> normtable = new List<List<normcell>>();
            public List<string> normtypes = new List<string>();
            public string numnorm = "linear";
            public normalizationTable(data data)
            {
                motherdata = data;
            }
            public void formdistinct()
            {
                foreach (List<string> list in motherdata.distinct)
                {
                    normtable.Add(new List<normcell>());
                    foreach (string str in list.Distinct())
                    {
                        normtable.Last().Add(new normcell());
                        normtable.Last().Last().raw = str;
                    }
                }
                for (int i=0;i<motherdata.Columns.Count;i++)
                {
                    if (motherdata.Columns[i][0]!="numeric")
                    {
                        if (normtable[i].Count == 2)
                            motherdata.Columns[i][0] = "bool";
                        if (normtable[i].Count == 3)
                            motherdata.Columns[i][0] = "triple";
                    }
                }
            }
            public void generateNorm()
            {
               // MessageBox.Show(normtable.Count.ToString() + " " + motherdata.Columns.Count.ToString());
                for (int i=0;i<normtable.Count;i++)
                {
                    normtypes.Add("");
                    if (motherdata.Columns[i][0]=="numeric")
                    {
                        if(numnorm=="linear")
                        {
                            normtypes[i] = numnorm;
                            foreach(normcell cel in normtable[i])
                            {
                                cel.norm=motherdata.linearNorm(cel.raw);
                            }
                            continue;
                        }
                    }
                    if (motherdata.Columns[i][0] == "bool")
                    {
                        normtable[i][0].norm.Add(-1);
                        normtable[i][1].norm.Add(1);
                        normtypes[i] = "bool";
                    }
                    if (motherdata.Columns[i][0] == "triple")
                    {
                        normtable[i][0].norm.Add(1);
                        normtable[i][0].norm.Add(0);
                        normtable[i][1].norm.Add(0);
                        normtable[i][1].norm.Add(1);
                        normtable[i][2].norm.Add(-1);
                        normtable[i][2].norm.Add(-1);
                        normtypes[i] = "triple";
                    } 
                    if (motherdata.Columns[i][0] == "categ")
                    {
                        for (int j=0;j<normtable[i].Count;j++)
                        {
                            for(int t=0;t<normtable[i].Count;t++)
                            {
                                if (j==t)
                                normtable[i][j].norm.Add(1);
                                else
                                normtable[i][j].norm.Add(0);
                            }
                        }
                        normtypes[i] = "categ";
                    }
                }
            }
        }
        class normcell
        {
            public string raw;
            public List<double> norm = new List<double>();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            network nnn = new network();
            nnn.readfromXML(@"C:\Users\User\Desktop\исклили2.xml", new int[] {3});
            nnn.totalgen();
            nnn.addEnter();
            nnn.layers[nnn.layers.Count-1].addNeuron("const", 1);
            nnn.addLayer(2);
            nnn.connectToPrev();
            nnn.layers[nnn.layers.Count - 1].addNeuron("const", 1);
            nnn.addOut();
            nnn.connectToPrev();
            nnn.setEnter(1);
            //nnn.getAnswer();
            //nnn.totalvisual();
            nnn.backPrograd();
            //nnn.getOutMistake(nnn.data.datarows[1]);
            nnn.setEnter(0);
            nnn.getAnswer();
            nnn.totalvisual("", true);
            nnn.setEnter(1);
            nnn.getAnswer();
            nnn.totalvisual("", true);
            nnn.setEnter(2);
            nnn.getAnswer();
            nnn.totalvisual("", true);
            nnn.setEnter(3);
            nnn.getAnswer();
            nnn.totalvisual("", true);
            /*nnn.normalize("linear");
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
            MessageBox.Show((nnn.normalizationTable[0][0] as ArrayList)[0].ToString());*/
        }
    }
}
