using System;
using System.Threading;
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
using System.Diagnostics;

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
        static void testgen(string file, int[] outs, Form form, int cicle=1000, bool parl=true)//многократное тестирование сетей
        {
            Stopwatch watch = new Stopwatch();
            int maxmist=0;
            int midmist = 0;
            int epoch = 0;
            int record = 99999999;
            double recordmist = 99999999;
            double midofmaxmist=0;
            watch.Start();
            if (parl)
            Parallel.For(0, cicle, (i, state) => {
                network testnet = new network(form);
                    testnet.min = -150;
                    testnet.max = 150;
                    testnet.speed = 4;
                    testnet.maxmist = 0.1;
                    testnet.epochs = 2000;
                    testnet.impuls = true;
                    testnet.smesh = false;
                    testnet.parall = false;
                testnet.readfromXML(file, outs);
                testnet.totalgen();
                testnet.addEnter();
                testnet.addLayer(6);
                testnet.connectToPrev();
                testnet.addOut();
                testnet.connectToPrev();
                string end = testnet.backPrograd();
                //testnet.totalvisual("", true);
                switch (end)
                {
                    case "Максимальная ошибка":
                        maxmist++;
                        midofmaxmist += testnet.epochpassed;
                        if (testnet.epochpassed < record) record = testnet.epochpassed;
                        if (testnet.learndata.maxmistake < recordmist) recordmist = testnet.learndata.maxmistake;
                        break;
                    case "Средняя ошибка":
                        midmist++;
                        break;
                    case "Предел эпох":
                        epoch++;
                        break;
                }
            });
            else
            for (int i = 0; i < cicle; i++)
            {
                network testnet = new network(form);
                    testnet.min = -150;
                    testnet.max = 150;
                    testnet.speed = 4;
                    testnet.maxmist = 0.1;
                    testnet.epochs = 2000;
                    testnet.impuls = true;
                    testnet.smesh = false;
                    testnet.parall = false;
                testnet.readfromXML(file, outs);
                testnet.totalgen();
                testnet.addEnter();
                testnet.addLayer(5);
                testnet.connectToPrev();
                testnet.addOut();
                testnet.connectToPrev();
                string end=testnet.backPrograd();
                //testnet.totalvisual("", true);
                switch (end)
                {
                    case "Максимальная ошибка":
                        maxmist++;
                        midofmaxmist += testnet.epochpassed;
                        if (testnet.epochpassed < record) record = testnet.epochpassed;
                        if (testnet.learndata.maxmistake < recordmist) recordmist = testnet.learndata.maxmistake;
                    break;
                    case "Средняя ошибка":
                        midmist++;
                    break;
                    case "Предел эпох":
                        epoch++;
                    break;
                }
                //testnet.totalvisual("", true);
            }
            watch.Stop();
            midofmaxmist = midofmaxmist / maxmist;

            string res = "Завершено по условиям:"+Environment.NewLine;
            res += "Максимальная ошибка: " + maxmist.ToString()+Environment.NewLine;
            res += "      Среднее количество эпох: " + (Math.Round(midofmaxmist, 0)).ToString() + Environment.NewLine;
            res += "      Минимальное количество эпох: " + (record).ToString() + Environment.NewLine;
            res += "      Максимальная ошибка: " + (recordmist).ToString() + Environment.NewLine;
            res += "Средняя ошибка: " + midmist.ToString() + Environment.NewLine;
            res += "Предел эпох: " + epoch.ToString() + Environment.NewLine;
            res += Environment.NewLine + "Затрачено времени " + watch.Elapsed.ToString() + Environment.NewLine;
            MessageBox.Show(res);

        }
        public Baseform()
        {
            InitializeComponent();
        }
        class network
        {
            public string name;
            // ЭЛЕМЕНТЫ НА ФОРМЕ
            Form baseform;
            Label epochsCount;
            // КОНЕЦ ЭЛЕМЕНТОВ
            public int min = -50;//значения для выставления начальных весов
            public int max = 50;
            public List<layer> layers=new List<layer>();//полный список слоев
            public List<neuron> neurons=new List<neuron>();//полный список нейронов
            public List<List<neuron>> enters = new List<List<neuron>>();//массив входов
            public List<List<neuron>> outs = new List<List<neuron>>();//массив выходов
            public bool parall=true;//разрешение на использование параллельности
            public double constant=1;//значение постоянных нейронов, ноль отключает их вообще
            public layer oute = new layer();//выходной слой
            public layer inn = new layer();//входной слой
            public List<double> comparer = new List<double>();
            public datarow currentrow;//текущая строка данных
            public double alpha = 1;//крутизна сигмоиды
            public string activationtype = "logistic";
            public string rawdata;//необработанная строка данных
            //public List<double> mistake;//сюда будем собирать ошибку
            //public List<List<List<object>>> data = new List<List<List<object>>>();//готовый к обучению массив с выборкой
            
            //public List<List<List<List<double>>>> normalizedData = new List<List<List<List<double>>>>();//массив нормализованных данных
            //public ArrayList[] normalizationTable;
            public data learndata;
            public bool outtable = true;//разрешение использовать для выходов отдельную таблицу нормализации
            public void readfromXML(string addr, int[] outss)
            {
                rawdata = System.IO.File.ReadAllText(@addr);
                Regex takerow = new Regex("<Row(.|\n)*?>(.|\n)*?</Row>");
                Regex takecell = new Regex("<Cell.*?>.*?<Data(.*?Type=\"(.*?)\"|.*?)>((.|\n)*?)</Data></Cell>");//2 - тип, 3 - данные
                learndata.distinct = new List<List<string>>();
                
                //for (int i = takecell.Matches(takerow.Match(rawdata).Groups[0].ToString()).Count; i > 0; i--) data.distinct.Add(new List<string>());
                //формируем параметры колонок
                foreach (Match match in takecell.Matches(takerow.Matches(rawdata)[0].Groups[0].ToString()))
                {
                    learndata.distinct.Add(new List<string>());
                    learndata.Columns.Add(new string[2]);
                    if (!outss.Contains(1+learndata.Columns.IndexOf(learndata.Columns.Last()))) 
                        learndata.Columns.Last()[1]="x-data";
                    else
                        learndata.Columns.Last()[1]="y-data";
                    int j;
                    if (Int32.TryParse(match.Groups[3].ToString(), out j))
                    {
                        learndata.Columns.Last()[0]="numeric";
                    }
                    else
                    {
                        learndata.Columns.Last()[0] = "categ";
                    }
                }
                foreach (Match match in takerow.Matches(rawdata))
                {
                    learndata.addRow();
                    foreach (Match cell in takecell.Matches(match.Groups[0].ToString()))
                    {
                        datacell cel=learndata.lastRow().addCell();
                        cel.data = cell.Groups[3].ToString(); ;
                        learndata.distinct[cel.columnNumber - 1].Add(cel.data);

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
                learndata.minmaxes();
                learndata.normtable.formdistinct();
                learndata.normtable.generateNorm();
                if(outtable)
                {
                    learndata.normtableForOuts.formdistinct();
                    learndata.normtableForOuts.generateNormForOuts();
                }
                learndata.generateNorm();
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
                layers[layers.Count - 1].connectLayer(layers[layers.Count - 2], min, max);
            }
            public layer addLayer(int neuronsnum = 0, string typ = "hidden")
            {
                layer lay = new layer(typ);
                lay.mothernetwork = this;
                for (int i = 0; i < neuronsnum; i++)
                {
                    neurons.Add(lay.addNeuron());
                    
                }
                layers.Add(lay);
                if (constant != 0) neurons.Add(lay.addNeuron("const", constant));
                return lay;
            }
            public void enterOut()
            {
                if (learndata.datarows.Count < 1 || learndata.distinct.Count < 1) { MessageBox.Show("Отсутствуют входные данные!"); return; }
                oute.type = "out";
                inn.type = "enter";
                for (int i = 0; i < learndata.Columns.Count;i++)
                {
                    if(learndata.Columns[i][1]=="x-data")
                    {
                        enters.Add(new List<neuron>());
                        foreach (double dd in learndata.normtable.normtable[i][0].norm)
                        {
                            enters.Last().Add(inn.addNeuron());
                        }
                        neurons.AddRange(enters.Last());
                    }
                    if (learndata.Columns[i][1] == "y-data")
                    {
                        outs.Add(new List<neuron>());
                        foreach (double dd in learndata.normtable.normtable[i][0].norm)
                        {
                            outs.Last().Add(oute.addNeuron());
                            comparer.Add(0);
                        }
                        neurons.AddRange(outs.Last());
                    }

                }
                if (constant != 0) neurons.Add(inn.addNeuron("const", constant));
            }
            public void addEnter()
            {
                inn.mothernetwork = this;
                layers.Add(inn);
            }
            public void addOut()
            {
                oute.mothernetwork = this;
                layers.Add(oute);
            }
            public network(Form form)
            {
                learndata = new data(this);
                baseform = form;
                epochsCount=baseform.Controls.Find("label1", false)[0] as Label;

            }
            public void setEnter(int rownum)
            {
                int neurocount=0;
                currentrow = learndata.datarows[rownum];
                foreach (datacell dat in learndata.datarows[rownum].datacells)
                {
                    if (learndata.Columns[dat.columnNumber-1][1]=="x-data")
                    foreach (double dd in dat.normalized)
                    {
                        inn.neurons[neurocount].Out = dd;
                        if (smesh) inn.neurons[neurocount].Out -= 0.5;
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
            public void backprogradepoch()//команды, выполняемые в каждой эпохе обратного распространение
            {
                foreach (datarow row in learndata.datarows)
                {
                    getOutMistake(row);
                    learndata.getmistakes();
                    totalvisual("ПОЛУЧЕНИЕ ОШИБКИ");
                    mistakeToEnter();
                    totalvisual("РАСПРОСТРАНЕНИЕ ОШИБКИ ВНИЗ");
                    weightCorrect();
                    totalvisual("РЕЗУЛЬТАТЫ КОРРЕКТИРОВКИ ВЕСОВ");
                }
                epochpassed++;
                speedcorr++;
                //epochsCount.Text = epochpassed.ToString();
                //epochsCount.Update();
                if (epochpassed>50000)//!!! боремся с параличем
                {
                    speed = 2 + speedcorr / 200000;
                }
            }
                public int epochs = 10000;//количество эпох для обучения
                public bool impuls = true;//разрешение на использоване импульса
                public bool smesh = false;//смещение для булевых значений и функции активации
                public double impulscoef = 0.9;
                public bool epochscond = true;
                public int epochpassed = 0;//количество пройденных эпох
                public double maxmist = 0.1;//условие по максимальной ошибке
                public bool maxmistcond =true;
                public double midmist = 0.1;//условие по средней ошибке
                public bool midmistcond = false;
                public double speed = 3;//скорость обучения
                public int speedcorr=0;
            public string backPrograd()
            {

                epochpassed = 0;
                /*while ((epochpassed < epochs || !epochscond) && (maxmist < data.maxmistake || !maxmistcond) && (midmist < data.midmistake || !midmistcond))
                {
                    backprogradepoch();
                }*/
                while (true)
                {
                    if (epochpassed >= epochs && epochscond) return "Предел эпох";
                    if (maxmist > learndata.maxmistake && maxmistcond) return "Максимальная ошибка";
                    if (midmist > learndata.midmistake && midmistcond) return "Средняя ошибка";
                    backprogradepoch();
                }
            }
            public void getOutMistake(datarow row)
            {
                    setEnter(learndata.datarows.IndexOf(row));
                    getAnswer();
                    row.findmistake(this);
                    totalvisual("ПЕРВИЧНЫЙ РЕЗУЛЬТАТ ПРОХОДА");
                    if (parall)
                    Parallel.ForEach(oute.neurons, (nn) => {
                        nn.q = nn.Out * (1 - nn.Out) * (row.outVector[oute.neurons.IndexOf(nn)] - nn.Out);
                    });
                    else
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
                    if (parall)
                    Parallel.ForEach(layers[i].neurons, (nn) => {
                        nn.takeMistake();
                    });
                    else
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
                    if (parall)
                    Parallel.ForEach(layers[i].neurons, (nn) => {
                        Parallel.ForEach(nn.sinapses, (val) =>
                        {
                            val.weight = val.weight + nn.q * speed * val.takeform.Out;
                            if (impuls)
                            {
                                val.weight += val.predcorr * impulscoef;
                                val.predcorr = nn.q * speed * val.takeform.Out;
                            }
                        });
                    });
                    else
                    foreach (neuron nn in layers[i].neurons)
                    {
                        foreach (sinaps val in nn.sinapses)
                        {
                            val.weight = val.weight + nn.q * speed * val.takeform.Out;
                            if (impuls)
                            {
                                val.weight += val.predcorr * impulscoef;
                                val.predcorr = nn.q * speed * val.takeform.Out;
                            }
                        }
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
                res += Environment.NewLine + "Максимальная ошибка: " + learndata.maxmistake.ToString() + ";  Средняя ошибка: " + learndata.midmistake.ToString()+Environment.NewLine;
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
                            weight += "  "+Math.Round(layers[lay].neurons[neu].sinapses[we].weight, round).ToString();
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
            public network mothernetwork;
            public layer(string typ = "hidden")
            {
                type=typ;
            }
            public neuron addNeuron(string typ="", double ou=0)
            {
                neuron neu=new neuron();
                neu.motherlayer = this;
                if (typ=="")
                neu.type = type;
                else
                neu.type = typ;
                neu.Out = ou;
                neurons.Add(neu);
                return neu;
            }
            public void connectLayer(layer lay, int min=-50, int max=50)// получает слой, от которого идут сигналы
            {
                foreach (neuron neu in this.neurons)
                {
                    if (neu.type == "const") break;
                    foreach (neuron ent in lay.neurons)
                    {
                        sinaps sin = new sinaps();
                        sin.father = neu;
                        sin.takeform = ent;
                        sin.weight = randomweight(min, max);
                        neu.sinapses.Add(sin);
                        ent.receivers.Add(neu);
                    }
                }
            }
            public void getOuts() //собирает все выходы нейронов, подключенных к нейронам данного слоя
            {
                if (mothernetwork.parall)
                Parallel.ForEach(neurons, (neu) => {
                    neu.takeNET();
                    neu.logistic(mothernetwork.alpha);
                });
                else
                foreach (neuron neu in neurons)
                {
                    neu.takeNET();
                    neu.logistic(mothernetwork.alpha);
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
            public layer motherlayer;
            public List<sinaps> sinapses = new List<sinaps>();
            public List<neuron> receivers = new List<neuron>();
            public void createsinaps(neuron nn, double weight=0) //получает нейрон, от которого будет передаваться значение
            {
                sinaps sin = new sinaps();
                sin.takeform = nn;
                sin.weight = weight;
                sin.father = this;
                sinapses.Add(sin);
                nn.receivers.Add(this);
            }
            public void takeNET()
            {
                if(type=="const")return;
                net = 0;
                if (motherlayer.mothernetwork.parall)
                Parallel.ForEach(sinapses, (taker) =>
                {
                    net += taker.takeform.Out * taker.weight;
                });
                else
                foreach (sinaps taker in sinapses)
                {
                    net+=taker.takeform.Out*taker.weight;
                }
            }
            public void takeMistake()
            {
                q=0;
                if (motherlayer.mothernetwork.parall)
                Parallel.ForEach(receivers, (nn) =>
                {
                    foreach (sinaps sin in nn.sinapses)
                    {
                        if (sin.takeform == this)
                        {
                            q += sin.weight * nn.q;
                            break;
                        }
                    }
                });
                else
                foreach (neuron nn in receivers)
                {
                    foreach(sinaps sin in nn.sinapses)
                    {
                        if(sin.takeform==this)
                        {
                            q += sin.weight * nn.q;
                            break;
                        }
                    }
                    //MessageBox.Show(nn.sinapses[this].ToString() + Environment.NewLine + nn.q.ToString());
                }
                q = q * Out * (1 - Out);
            }
            public void logistic(double alpha=1)
            {
                if (type == "const") return;
                Out=1/(1+Math.Pow(Math.E, -net*alpha));
                //if (motherlayer.mothernetwork.smesh) Out -= 0.5;
            }
        }
        class sinaps
        {
            public double weight;
            public double predcorr = 0;
            public neuron father;
            public neuron takeform;
        }
        class data
        {
            public network mothernetwork;
            public List<datarow> datarows = new List<datarow>();
            public List<string[]> Columns = new List<string[]>();//1 - тип, 2 - роль в выборке
            public List<List<string>> distinct;//используется для определения различных элементов в каждом столбце.
            public double min=9999999999;
            public double max=-9999999999;
            public double maxmistake=Double.PositiveInfinity;//максимальная ошибка по выборке
            public double midmistake = Double.PositiveInfinity;//средняя ошибка по выборке
            public normalizationTable normtable;
            public normalizationTable normtableForOuts;
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
                        if (!mothernetwork.outtable)
                        {
                            foreach (normcell cell in normtable.normtable[cel.columnNumber - 1])
                            {
                                if (cell.raw == cel.data)
                                    cel.normalized = cell.norm;
                            }
                        }
                        else
                        {
                            foreach (normcell cell in normtableForOuts.normtable[cel.columnNumber - 1])
                            {
                                if (cell.raw == cel.data)
                                    cel.normalized = cell.norm;
                            }
                        }
                    }
                    row.generateOutVector();
                }
            }
            public void getmistakes()
            {
                maxmistake = 0;
                midmistake = 0;
                if (mothernetwork.parall)
                Parallel.ForEach(datarows, (row) =>
                {
                    midmistake += row.mistake;
                    if (row.mistake > maxmistake) maxmistake = row.mistake;
                });
                else
                foreach(datarow row in datarows)
                {
                    midmistake += row.mistake;
                    if (row.mistake > maxmistake) maxmistake = row.mistake;
                }
                midmistake = midmistake / datarows.Count;
            }
            public data(network mom)
            {
                normtable = new normalizationTable(this);
                normtableForOuts = new normalizationTable(this);
                mothernetwork = mom;
            }
        }
        class datarow
        {
            public List<datacell> datacells = new List<datacell>();
            public List<double> outVector = new List<double>();
            public double mistake=0;//последняя ошибка по строке
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
            public void findmistake(network net)
            {
                if (motherData.mothernetwork.parall)
                Parallel.ForEach(datacells, (cell) =>
                {
                    mistake = Math.Abs(outVector[0] - net.oute.neurons[0].Out);
                });
                else
                foreach (datacell cell in this.datacells)
                {
                    mistake = Math.Abs(outVector[0] - net.oute.neurons[0].Out);
                }
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
                        if (normtable[i].Count == 3)
                            motherdata.Columns[i][0] = "triple";
                    }
                    if (normtable[i].Count == 2)
                        motherdata.Columns[i][0] = "bool";
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
            public void generateNormForOuts()
            {
                // MessageBox.Show(normtable.Count.ToString() + " " + motherdata.Columns.Count.ToString());
                for (int i = 0; i < normtable.Count; i++)
                {
                    normtypes.Add("");
                    if (motherdata.Columns[i][0] == "numeric")
                    {
                        if (numnorm == "linear")
                        {
                            normtypes[i] = numnorm;
                            foreach (normcell cel in normtable[i])
                            {
                                cel.norm = motherdata.linearNorm(cel.raw);
                            }
                            continue;
                        }
                    }
                    if (motherdata.Columns[i][0] == "bool")
                    {
                        normtable[i][0].norm.Add(0);
                        normtable[i][1].norm.Add(1);
                        normtypes[i] = "bool";
                    }
                    if (motherdata.Columns[i][0] == "triple")
                    {
                        normtable[i][0].norm.Add(1);
                        normtable[i][0].norm.Add(0);
                        normtable[i][1].norm.Add(0);
                        normtable[i][1].norm.Add(1);
                        normtable[i][2].norm.Add(0);
                        normtable[i][2].norm.Add(0);
                        normtypes[i] = "triple";
                    }
                    if (motherdata.Columns[i][0] == "categ")
                    {
                        for (int j = 0; j < normtable[i].Count; j++)
                        {
                            for (int t = 0; t < normtable[i].Count; t++)
                            {
                                if (j == t)
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

            testgen(@"C:\Users\User\Desktop\исклили2.xml", new int[] { 3 }, this);
            /*network nnn = new network();
            nnn.readfromXML(@"C:\Users\User\Desktop\исклили2.xml", new int[] {3});
            nnn.totalgen();
            nnn.addEnter();
            nnn.addLayer(2);
            nnn.connectToPrev();
            nnn.addOut();
            nnn.connectToPrev();
            nnn.setEnter(1);
            nnn.getAnswer(); 
            nnn.totalvisual("НАЧАЛЬНОЕ СОСТОЯНИЕ", true);
            nnn.backPrograd();
            //nnn.getOutMistake(nnn.data.datarows[1]);
            /*nnn.setEnter(0);
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
            MessageBox.Show(nnn.epochpassed.ToString()+" эпох");
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
