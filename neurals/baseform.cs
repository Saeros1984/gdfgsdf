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
        public delegate void multil(string file, int[] outs);
        static Random rand = new Random();
        // CONTROLS
        public static Baseform bas;
        public static ListBox brainlist;
        public static Button startLearning;
        public static Label name;
        public static Label passedepochs;
        public static Label maxmist;
        public static Label midmist;
        public static Label interrupt;
        public static Label Speed;
        public static DataGridView userinput;
        public static DataGridView useroutput;
        public static Label tbegin;
        public static Label tcurrent;
        public static Label stepvar;
        public static Label gradientpercent;
        public static Label pspeed;
        public static Label result;
        public static Control getControl(string name)
        {
            return bas.Controls.Find(name, true)[0];
        }
        static network currentNetwork = null;
        static void renderNet ()
        {
            name.Text = currentNetwork.name + " " + currentNetwork.suffix.ToString();
        }
        static void renderLearn()
        {
            passedepochs.Invoke(new Action(() => passedepochs.Text = currentNetwork.epochpassed.ToString()));
            passedepochs.Invoke(new Action(() => passedepochs.Update()));
            maxmist.Invoke(new Action(() => maxmist.Text = currentNetwork.learndata.maxmistake.ToString()));
            maxmist.Invoke(new Action(() => maxmist.Update()));
            midmist.Invoke(new Action(() => midmist.Text = currentNetwork.learndata.midmistake.ToString()));
            midmist.Invoke(new Action(() => midmist.Update()));
            tbegin.Invoke(new Action(() => tbegin.Text = currentNetwork.T0.ToString()));
            tbegin.Invoke(new Action(() => tbegin.Update()));
            tcurrent.Invoke(new Action(() => tcurrent.Text = currentNetwork.T.ToString()));
            tcurrent.Invoke(new Action(() => tcurrent.Update()));
            stepvar.Invoke(new Action(() => stepvar.Text = currentNetwork.Px.ToString()));
            stepvar.Invoke(new Action(() => stepvar.Update()));
            interrupt.Invoke(new Action(() => interrupt.Text = currentNetwork.interrupted));
            Speed.Invoke(new Action(() => Speed.Text = currentNetwork.speed.ToString()));
            gradientpercent.Invoke(new Action(() => gradientpercent.Text = currentNetwork.koshicoeff.ToString()));
            pspeed.Invoke(new Action(() => pspeed.Text = currentNetwork.p.ToString()));
            if (currentNetwork.learning == false)
                startLearning.Invoke(new Action(() => startLearning.Text = "Начать обучение"));
            else
                startLearning.Invoke(new Action(() => startLearning.Text = "Остановить обучение"));
        }
        static void renderUserdata()
        {
            userinput.ColumnCount = 0;
            //userinput.Columns[1].HeaderText = "mmmmmm";
        }
        static void onnetchange(string name)
        {
            currentNetwork = brain.networks[0];
            foreach (network nn in brain.networks)
            {
                if (nn.name + " " + nn.suffix.ToString() == name)
                {
                    currentNetwork = nn;
                    break;
                }
            }
            renderNet();
            renderLearn();
        }
        public class parameters
        {
            public string path;
            public int[] outs;
            public int cycle;
            public bool parl;
        }
        public static object paramGen(string path, int[] outs, int cycle, bool parl)
        {
            parameters param=new parameters();
            param.path = path;
            param.outs = outs;
            param.cycle = cycle;
            param.parl = parl;
            return param;
        }
        static class brain
        {
            public static List<network> networks = new List<network>();
            public delegate void brainListSet();
            public static void setList()
            {
                //brainlist.Invoke(new brainListSet(brainlist.Items.Clear));
                brainlist.BeginInvoke(new Action(() =>
                {
                    brainlist.Items.Clear(); 
                    for (int i = 0; i <= brain.networks.Count - 1; i++)
                    {
                        brainlist.Items.Add(brain.networks[i].name + " " + brain.networks[i].suffix.ToString());
                        brainlist.Update();
                    }
                    brainlist.BeginInvoke(new brainListSet(brainlist.Update));
                    brainlist.Update();
                }));
            }
        }
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
        public static void testgen(/*string file, int[] outs, int cicle=5, bool parl=true*/ object obj)//многократное тестирование сетей
        {
            parameters param=obj as parameters;
            Stopwatch watch = new Stopwatch();
            int maxmist=0;
            int midmist = 0;
            int epoch = 0;
            int record = 99999999;
            double recordmist = 99999999;
            double midofmaxmist=0;
            watch.Start();
            if (param.parl)
            Parallel.For(0, param.cycle, (i, state) => {
                network testnet = new network();
                    testnet.min = -150;
                    testnet.max = 150;
                    testnet.speed = 1;
                    testnet.maxmist = 0.00035;
                    testnet.epochs = 3000;
                    testnet.impuls = true;
                    testnet.impulscoef = 0.9;
                    testnet.smesh = false;
                    testnet.parall = false;
                    testnet.alpha = 0.03;
                    testnet.numericnorm = "Логистическая";
                    testnet.normalalpha = 0.03;
                testnet.readfromXML(param.path, param.outs);
                testnet.totalgen();
                testnet.addEnter();
                testnet.addLayer(10);
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
            for (int i = 0; i < param.cycle; i++)
            {
                network testnet = new network();
                    testnet.min = -150;
                    testnet.max = 150;
                    testnet.speed = 4;
                    testnet.maxmist = 0.1;
                    testnet.epochs = 2000;
                    testnet.impuls = true;
                    testnet.smesh = false;
                    testnet.parall = false;
                testnet.readfromXML(param.path, param.outs);
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
            bas = this;
            brainlist = bas.Controls.Find("brainList", false)[0] as ListBox;
            name = getControl("networkName") as Label;
            passedepochs = getControl("epochpassed") as Label;
            maxmist = getControl("maxmistake") as Label;
            midmist = getControl("midmistake") as Label;
            interrupt = getControl("interrupted") as Label;
            Speed = getControl("speed") as Label;
            startLearning = getControl("StartLearning") as Button;
            userinput = getControl("userInput") as DataGridView;
            useroutput = getControl("userOutput") as DataGridView;
            tbegin = getControl("TBegin") as Label;
            tcurrent = getControl("TCurrent") as Label;
            stepvar = getControl("StepVar") as Label;
            gradientpercent = getControl("gradientPercent") as Label;
            pspeed = getControl("Pspeed") as Label;
            result = getControl("Result") as Label;
        }
        class network
        {
            public string name="";
            public string descr = "";
            public int suffix = 0;
            // ЭЛЕМЕНТЫ НА ФОРМЕ
            Label epochsCount;
            // КОНЕЦ ЭЛЕМЕНТОВ
            public Thread learningThread;
            public bool learning = false;//показывает, идет ли обучение в данный момент
            public int min = -50;//значения для выставления начальных весов
            public int max = 50;
            public bool regular = true;//регуляризация
            public double regweight = 8;//преельные веса для регуляризации
            public double forfeit=100;//штраф-делитель для регуляризации
            public List<layer> layers=new List<layer>();//полный список слоев
            public List<neuron> neurons=new List<neuron>();//полный список нейронов
            public List<List<neuron>> enters = new List<List<neuron>>();//массив входов
            public List<List<neuron>> outs = new List<List<neuron>>();//массив выходов
            public bool control = false;//разрешение контрольной выборки
            public bool parall=false;//разрешение на использование параллельности
            public double constant=1;//значение постоянных нейронов, ноль отключает их вообще
            public layer oute = new layer();//выходной слой
            public layer inn = new layer();//входной слой
            public List<double> comparer = new List<double>();
            public datarow currentrow;//текущая строка данных
            public double alpha = 1;//крутизна сигмоиды
            public string activationtype = "Логистическая";
            public string numericnorm = "Линейная";//тип нормализации для чисел
            public double normalalpha = 0.1;//крутизна сигмоиды для нормализации чисел
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
                if (control)
                    learndata.generateControl();
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
            public network(string name="Новая сеть")
            {
                this.name = name;
                if (currentNetwork == null) currentNetwork = this;
                learndata = new data(this);
                epochsCount=bas.Controls.Find("label1", false)[0] as Label;
                bool same = false;
                do
                {
                    foreach (network nn in brain.networks)
                    {
                        if (this.name + " " + this.suffix.ToString() == nn.name + " " + nn.suffix.ToString())
                        {
                            this.suffix += 1;
                            same = true;
                            break;
                        }
                        same = false;
                    }
                    
                }
                while (same);
                brain.networks.Add(this);
                brain.setList();
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
                if (koshicoeff == 0) return;
                foreach (datarow row in learndata.datarows)
                {
                    if (random)
                    {
                        getOutMistake(learndata.datarows[(int)(randomweight(0, learndata.datarows.Count - 1) * 100)]);
                    }
                    else
                    {
                        getOutMistake(row);
                    }
                    if (control && row.type == "learn" || !control)
                    {
                        totalvisual("ПОЛУЧЕНИЕ ОШИБКИ");
                        mistakeToEnter();
                        totalvisual("РАСПРОСТРАНЕНИЕ ОШИБКИ ВНИЗ");
                        weightCorrect();
                        totalvisual("РЕЗУЛЬТАТЫ КОРРЕКТИРОВКИ ВЕСОВ");
                    }
                    if (learning == false)
                    {
                        renderLearn();
                        break;
                    }
                    learndata.getmistakes();
                }
                //epochsCount.Text = epochpassed.ToString();
                //epochsCount.Update();
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
                public double speed = 5;//скорость обучения
                public int speedcorr=0;
                public string interrupted = "Обучение еще не проводилось";
                public bool random = false;//подача выборки в случайном порядке
                public double koshicoeff = 0.5;
            public string backPrograd()
            {

                /*while ((epochpassed < epochs || !epochscond) && (maxmist < data.maxmistake || !maxmistcond) && (midmist < data.midmistake || !midmistcond))
                {
                    backprogradepoch();
                }*/
                learning = true;
                while (true)
                {
                    if (epochpassed >= epochs && epochscond) { learning = false; return "Предел эпох"; }
                    if (maxmist > learndata.maxmistake && maxmistcond) { learning = false; return "Максимальная ошибка"; }
                    if (midmist > learndata.midmistake && midmistcond) { learning = false; return "Средняя ошибка"; }
                    stohasticepoch();
                    backprogradepoch();
                    epochpassed++;
                    speedcorr++;
                    if (learning == false) return "Остановлено пользователем";
                    if (currentNetwork == this&&epochpassed%50==0) renderLearn();
                }

            }
            public string stohastic()
            {
                T = T0 / (1 + epochpassed);
                while (true)
                {
                    if (epochpassed >= epochs && epochscond) { learning = false; return "Предел эпох"; }
                    if (maxmist > learndata.maxmistake && maxmistcond) { learning = false; return "Максимальная ошибка"; }
                    if (midmist > learndata.midmistake && midmistcond) { learning = false; return "Средняя ошибка"; }
                    stohasticepoch();
                    epochpassed++;
                    if (learning == false) return "Остановлено пользователем";
                    if (currentNetwork == this && epochpassed % 50 == 0) renderLearn();
                }
            }
            public double T0 = 3000;//начальная температура
            public double T;//текущая температура
            public double p = 0.001;//скорость обучения
            public double xc;//величина текущего шага
            public double mist = 0;//ошибка для всей выборки
            public double Px = 0;//вероятность шага величины
            public double b = 0.00001;//множитель времени
            public void stohasticepoch()
            {
                if ((1-koshicoeff) == 0) return;
                xc = (1-koshicoeff)*p * T * Math.Tanh(randomweight(-157, 157));
                T = T0 / (1 + (double)epochpassed*b);
                Px = T / (T*T+xc*xc);
                //MessageBox.Show(T.ToString());
                mist = learndata.maxmistake;
                foreach (datarow row in learndata.datarows)
                {
                    if (random)
                    {
                        getOutMistake(learndata.datarows[(int)(randomweight(0, learndata.datarows.Count - 1) * 100)]);
                    }
                    else
                    {
                        getOutMistake(row);
                    }

                    if (learning == false)
                    {
                        renderLearn();
                        break;
                    }
                    row.findmistake(this);
                   
                    getAnswer();
                    row.findmistake(this);
                    learndata.getmistakes();
                }
                //epochpassed++;
                //speedcorr++;
                if (mist != 0 && mist < learndata.maxmistake && randomweight(0,100)>Px) stohasticreturnweight();
                stohasticweightcorrect(xc);
            }
            public void stohasticWepoch()
            {
                xc = speed * T * Math.Tanh(randomweight(-157, 157));
                T = T0 / (1 + (double)epochpassed / 10000);
                Px = T / T0;
                //MessageBox.Show(T.ToString());
                mist = learndata.maxmistake;
                foreach (datarow row in learndata.datarows)
                {                  
                    row.findmistake(this);
                    mist=row.mistake;
                    getOutMistake(row);
                    foreach(neuron neu in neurons)
                    {
                        foreach (sinaps sin in neu.sinapses)
                        {
                            setEnter(learndata.datarows.IndexOf(row));
                            getAnswer();
                            row.findmistake(this);
                            mist=row.mistake;
                            sin.previous = sin.weight;
                            sin.weight+=xc;
                            setEnter(learndata.datarows.IndexOf(row));
                            getAnswer();
                            row.findmistake(this);
                            if (row.mistake>=mist)
                            {
                                sin.weight = sin.previous;
                            }
                        }
                    }
                    
                }
                learndata.getmistakes();
                epochpassed++;
                speedcorr++;
                if (mist != 0 && mist < learndata.maxmistake && randomweight(0, 100) > Px) stohasticreturnweight();
                stohasticweightcorrect(xc);
            }
            public void stohasticweightcorrect(double corr=0)
            {
                for (int i = layers.Count - 1; i > 0; i--)
                {
                    foreach (neuron nn in layers[i].neurons)
                    {
                        foreach (sinaps val in nn.sinapses)
                        {
                            double cor=corr*randomweight(-100, 100);
                            val.previous = val.weight;
                            if (regular && Math.Abs(val.weight + cor) > regweight)
                            {
                                val.weight += cor / forfeit;
                            }
                            else
                            {
                                val.weight += cor;
                            }
                        }
                    }
                }
            }
            public void stohasticreturnweight()
            {
                for (int i = layers.Count - 1; i > 0; i--)
                {
                    foreach (neuron nn in layers[i].neurons)
                    {
                        foreach (sinaps val in nn.sinapses)
                        {
                            val.weight = val.previous;
                        }
                    }
                }
            }
            public void startLearning()
            {
                learning = true;
                learningThread = new Thread(forthread);
                learningThread.Start(this);
            }
            public void startCoshi()
            {
                learning = true;
                learningThread = new Thread(forcoshi);
                learningThread.Start(this);
            }
            public static void forthread(object netw)
            {
                network thisnet = netw as network;
                thisnet.interrupted = thisnet.backPrograd();
                renderLearn();
            }
            public static void forcoshi(object netw)
            {
                network thisnet = netw as network;
                thisnet.interrupted = thisnet.stohastic();
                renderLearn();
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
                        string activat;
                        if (oute.neurons[i].activ == "inherit")
                        {
                            if (oute.neurons[i].motherlayer.activ=="inherit")
                            {
                                activat = activationtype;
                            }
                            else
                            {
                                activat = oute.neurons[i].motherlayer.activ;
                            }
                        }
                        else
                            activat = oute.neurons[i].activ;
                        ////////////////////// ВЫШЕ мы уточняем, мкакая функция активации у конкретногго нейрона
                        if (activat == "Логистическая")
                        oute.neurons[i].q = oute.neurons[i].Out * (1 - oute.neurons[i].Out) * (row.outVector[i] - oute.neurons[i].Out);
                        if (activat == "Линейная")
                        oute.neurons[i].q = alpha * (row.outVector[i] - oute.neurons[i].Out);
                        if (activat == "Гипертангенс")
                        oute.neurons[i].q = (1 - Math.Pow(oute.neurons[i].Out, 2)) * (row.outVector[i] - oute.neurons[i].Out);
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
                                val.weight += val.predcorr * impulscoef + nn.q * speed * val.takeform.Out;
                                val.predcorr = val.predcorr * impulscoef+nn.q * speed * val.takeform.Out;
                            }
                        });
                    });
                    else
                    foreach (neuron nn in layers[i].neurons)
                    {
                        foreach (sinaps val in nn.sinapses)
                        {
                            val.weight = val.weight + nn.q * speed * val.takeform.Out*koshicoeff;
                            if (impuls)
                            {
                                val.weight += val.predcorr * impulscoef*koshicoeff;
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
            public string visualNormalized()
            {
                string res = "";
                foreach (string[] st in learndata.Columns)
                {
                    res += st[0] + " " + Environment.NewLine;
                }
                foreach (datarow row in learndata.datarows)
                {
                    foreach (datacell cell in row.datacells)
                    {
                        res += cell.data + " " + Math.Round(cell.normalized[0], 5).ToString()+"    ";
                    }
                    res += Environment.NewLine;
                }
                return res;
            }
            public string visualNormtable()
            {
                string res = "";
                foreach(List<normcell> lcel in learndata.normtable.normtable)
                {
                    foreach (normcell nor in lcel)
                    {
                        foreach (Double d in nor.norm)
                        {
                            res += d.ToString() + " ";
                        }
                    }
                    res += Environment.NewLine;
                }
                return res;
            }
            public void initialweights()
            {
                foreach (neuron nn in neurons)
                {
                    foreach (sinaps sin in nn.sinapses)
                    {
                        sin.weight = randomweight(min, max);
                    }
                }
            }
            public void multiplelearning(int num=100)
            {
                network nnn = new network();
                string res="";
                int maxmist = 0;
                int midmist = 0;
                int epoch = 0;
                int record = 99999999;
                double recordmist = 99999999;
                double midofmaxmist = 0;
                for(int i=0;i<num;i++)
                {
                    nnn = new network();
                    nnn.readfromXML(@"C:\Users\User\Desktop\исклили.xml", new int[] { 3 });
                    nnn.totalgen();
                    nnn.addEnter();
                    nnn.addLayer(2);
                    nnn.connectToPrev();
                    //nnn.lastLayer().activ = "Линейная";
                    nnn.addOut();
                    nnn.connectToPrev();
                    nnn.initialweights();
                    string end = nnn.backPrograd();
                    switch (end)
                    {
                        case "Максимальная ошибка":
                            maxmist++;
                            midofmaxmist += nnn.epochpassed;
                            if (epochpassed < record) record = epochpassed;
                            if (nnn.learndata.maxmistake < recordmist) recordmist = nnn.learndata.maxmistake;
                            break;
                        case "Средняя ошибка":
                            midmist++;
                            break;
                        case "Предел эпох":
                            epoch++;
                            break;
                    }
                    res = "Максимальная ошибка: " + maxmist.ToString() + Environment.NewLine;
                    res += "      Среднее количество эпох: " + (Math.Round(midofmaxmist/maxmist, 0)).ToString() + Environment.NewLine;
                    res += "      Минимальное количество эпох: " + (record).ToString() + Environment.NewLine;
                    res += "      Максимальная ошибка: " + (recordmist).ToString() + Environment.NewLine;
                    res += "Средняя ошибка: " + midmist.ToString() + Environment.NewLine;
                    res += "Предел эпох: " + epoch.ToString() + Environment.NewLine;
                    result.Invoke(new Action(() => result.Text = res));
                    result.Invoke(new Action(() => result.Update()));
                }
            }
        }
        class layer
        {
            public List<neuron> neurons=new List<neuron>();
            public string type;
            public string activ = "inherit";
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
                    if (neu.type == "const") return;
                    string activat;
                    if (neu.activ == "inherit")
                    {
                        if (activ == "inherit")
                        {
                            activat = mothernetwork.activationtype;
                        }
                        else
                        {
                            activat = activ;
                        }
                    }
                    else
                        activat = neu.activ;
                    //////////////////
                    neu.takeNET();
                    if (activat == "Логистическая")
                    neu.logistic(mothernetwork.alpha);
                    if (activat == "Линейная")
                    neu.linear(mothernetwork.alpha);
                    if (activat == "Гипертангенс")
                    neu.hypertang(mothernetwork.alpha);
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
            public string activ = "inherit";
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
                string activat;
                if (activ == "inherit")
                {
                    if (motherlayer.activ == "inherit")
                    {
                        activat = motherlayer.mothernetwork.activationtype;
                    }
                    else
                    {
                        activat = motherlayer.activ;
                    }
                }
                else
                    activat = activ;
                /////////////////////////////
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
                if(activat=="Логистическая")
                q = q * Out * (1 - Out);
                if (activat == "Линейная")
                q = q * motherlayer.mothernetwork.alpha;
                if (activat == "Гипертангенс")
                q = q * (1 - Math.Pow(Out, 2));
            }
            public void logistic(double alpha=1)
            {
                if (type == "const") return;
                Out=1/(1+Math.Pow(Math.E, -net*alpha));
                //if (motherlayer.mothernetwork.smesh) Out -= 0.5;
            }
            public void linear(double alpha = 1)
            {
                if (type == "const") return;
                Out = net*alpha;
            }
            public void hypertang(double a=1)
            {
                Out = (Math.Pow(Math.E, net * a) - Math.Pow(Math.E, -net * a)) / (Math.Pow(Math.E, net * a) + Math.Pow(Math.E, -net * a));
            }
       }
        class sinaps
        {
            public double weight;
            public double predcorr = 0;
            public double previous = 0;
            public neuron father;
            public neuron takeform;
        }
        class data
        {
            public network mothernetwork;
            public List<datarow> datarows = new List<datarow>();
            public List<string[]> Columns = new List<string[]>();//1 - тип, 2 - роль в выборке
            public List<List<string>> distinct;//используется для определения различных элементов в каждом столбце.
            public int controlnum = 25;
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
                             ii=Double.Parse(str.Replace(".", ","));
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
                oo.Add((Double.Parse(enter.Replace(".",",")) - min) / (max - min));
                return oo;
            }
            public List<double> logisticNorm(string enter)
            {
                List<double> oo = new List<double>();
                oo.Add(1 / (1 + Math.Pow(Math.E, -Double.Parse(enter.Replace(".", ",")) * mothernetwork.normalalpha)));
                return oo;
            }
            public List<double> hypertangNorm(string enter)
            {
                List<double> oo = new List<double>();
                Double a = mothernetwork.normalalpha;
                double ent = Double.Parse(enter.Replace(".", ","));
                oo.Add((Math.Pow(Math.E, ent * a) - Math.Pow(Math.E, -ent * a)) / (Math.Pow(Math.E, ent * a) + Math.Pow(Math.E, -ent * a)));
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
                                MessageBox.Show(cel.normalized.ToString()+" "+  cell.norm.ToString()+"    ");
                            }
                        }
                        else
                        {
                            foreach (normcell cell in normtable.normtable[cel.columnNumber - 1])
                            {
                                if (cell.raw == cel.data)
                                    cel.normalized = cell.norm;
                            }
                            foreach (normcell cell in normtableForOuts.normtable[cel.columnNumber - 1])
                            {
                                if (cell.raw == cel.data&&Columns[cel.columnNumber-1][1]=="y-data")
                                    cel.normalized = cell.norm;
                            }
                        }
                    }
                    row.generateOutVector();
                }
            }
            public void generateControl()
            {
                for (int i=0; i<Math.Round((decimal)datarows.Count/100*controlnum, 0); i++)
                {
                    double rand = randomweight(0, datarows.Count-1)*100;
                    while(datarows[(int)rand].type!="learn")
                    {
                        rand = randomweight(0, datarows.Count - 1) * 100;
                    }
                    datarows[(int)rand].type = "control";
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
                    if (mothernetwork.control)
                    {
                        if (row.type == "control")
                        {
                            midmistake += row.mistake;
                            if (row.mistake > maxmistake) maxmistake = row.mistake;
                        }
                    }
                    else
                    {
                        midmistake += row.mistake;
                        if (row.mistake > maxmistake) maxmistake = row.mistake;
                    }
                }
                midmistake = midmistake / datarows.Count;
            }
            public data(network mom)
            {
                mothernetwork = mom;
                normtable = new normalizationTable(this);
                normtableForOuts = new normalizationTable(this);
            }
        }
        class datarow
        {
            public List<datacell> datacells = new List<datacell>();
            public List<double> outVector = new List<double>();
            public double mistake=0;//последняя ошибка по строке
            public string type = "learn";
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
            public data motherdata;
            public List<List<normcell>> normtable = new List<List<normcell>>();
            public List<string> normtypes = new List<string>();
            public string numnorm = "linear";
            public normalizationTable(data data)
            {
                motherdata = data;
                numnorm = motherdata.mothernetwork.numericnorm;
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
                        if (numnorm == "Без нормализации")
                        {
                            normtypes[i] = numnorm;
                            foreach (normcell cel in normtable[i])
                            {
                                cel.norm.Add(Double.Parse(cel.raw));
                            }
                            continue;
                        }
                        if(numnorm=="Линейная")
                        {
                            normtypes[i] = numnorm;
                            foreach(normcell cel in normtable[i])
                            {
                                cel.norm=motherdata.linearNorm(cel.raw);
                            }
                            continue;
                        }
                        if (numnorm == "Логистическая")
                        {
                            normtypes[i] = numnorm;
                            foreach (normcell cel in normtable[i])
                            {
                                cel.norm = motherdata.logisticNorm(cel.raw);
                            }
                            continue;
                        }
                        if (numnorm == "Гипертангенс")
                        {
                            normtypes[i] = numnorm;
                            foreach (normcell cel in normtable[i])
                            {
                                cel.norm = motherdata.hypertangNorm(cel.raw);
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
                        if (numnorm == "Без нормализации")
                        {
                            normtypes[i] = numnorm;
                            foreach (normcell cel in normtable[i])
                            {
                                cel.norm.Add(Double.Parse(cel.raw));
                            }
                            continue;
                        }
                        if (numnorm == "Линейная")
                        {
                            normtypes[i] = numnorm;
                            foreach (normcell cel in normtable[i])
                            {
                                cel.norm = motherdata.linearNorm(cel.raw);
                            }
                            continue;
                        }
                        if (numnorm == "Логистическая")
                        {
                            normtypes[i] = numnorm;
                            foreach (normcell cel in normtable[i])
                            {
                                cel.norm = motherdata.logisticNorm(cel.raw);
                            }
                            continue;
                        }
                        if (numnorm == "Гипертангенс")
                        {
                            normtypes[i] = numnorm;
                            foreach (normcell cel in normtable[i])
                            {
                                cel.norm = motherdata.hypertangNorm(cel.raw);
                            }
                            continue;
                        }
                    }
                    if (motherdata.Columns[i][0] == "bool")
                    {
                        if(motherdata.mothernetwork.activationtype =="Гипертангенс")
                        normtable[i][0].norm.Add(-1);
                        else
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

            //new Thread(new ParameterizedThreadStart(testgen)).Start(paramGen(@"C:\Users\User\Desktop\исклили2.xml", new int[] { 3 }, 20, true));
            network nnn = new network("sadf");
            nnn.readfromXML(@"C:\Users\User\Desktop\исклили.xml", new int[] {3});
            nnn.totalgen();
            nnn.addEnter();
            nnn.addLayer(2);
            nnn.connectToPrev();
            //nnn.lastLayer().activ = "Линейная";
            nnn.addOut();
            nnn.connectToPrev();
            renderUserdata();
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

        private void brainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            onnetchange(brainlist.SelectedItem.ToString());
        }

        private void StartLearning_Click(object sender, EventArgs e)
        {
            if (currentNetwork.learning==false)
            {
                currentNetwork.learning = true;
                currentNetwork.startLearning();
            }
            else
            {
                currentNetwork.learning = false;
            }
            Baseform.renderLearn();
        }
        private void koshi_Click(object sender, EventArgs e)
        {
            if (currentNetwork.learning == false)
            {
                currentNetwork.learning = true;
                currentNetwork.startCoshi();
            }
            else
            {
                currentNetwork.learning = false;
            }
            Baseform.renderLearn();
        }

        private void speedNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                currentNetwork.speed = (Double)(sender as NumericUpDown).Value;
                renderLearn();
            }
        }

        private void visualButton_Click(object sender, EventArgs e)
        {
            currentNetwork.totalvisual("", true);
        }

        private void TBeginNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                currentNetwork.T0 = (Double)(sender as NumericUpDown).Value;
                renderLearn();
            }
        }

        private void gradientPercentNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                currentNetwork.koshicoeff = (Double)(sender as NumericUpDown).Value;
                renderLearn();
            }
        }

        private void PspeedNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                currentNetwork.p = (Double)(sender as NumericUpDown).Value;
                renderLearn();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentNetwork.multiplelearning(100);
        }
    }
}
