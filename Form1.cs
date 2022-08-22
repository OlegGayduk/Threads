using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace lab2
{
    public partial class Form1 : Form
    {
        String parentTask = "Кнопка";
        Stopwatch stopwatch = new Stopwatch();
        //объявляем потоки
        Thread A; 
        Thread B;
        Thread C;
        Thread D;
        Thread E;
        Thread F;
        Thread H;
        Thread G;
        Thread K;
        Font font = new Font("Arial", 9);
        SolidBrush brush = new SolidBrush(Color.Black); //кисть
        string log = ""; // строка для лога
        int minutes = 0, sec = 0, msec = 0; //минуты, секунлы и миллисекунды 
        Pen pen = new Pen(Color.Black);
        Random rnd = new Random(); //объявляем random
        int n = 4; // задаем n в рекомендованном диапазоне от 3 до 6 (по заданию) 
        int[] M1; // первый массив (по заданию)
        int[] M2; //второй массив (по заданию)
        //вспомогательные массивы и переменные (по заданию)
        List<int> f1 = new List<int>();
        List<int> f2 = new List<int>();
        List<int> f3 = new List<int>();
        List<int> f4 = new List<int>();
        int f5 = 0;
        int f6 = 0;
        int f7 = 0;
        int f8 = 0;
        float height; // высота контейнера (pictureBox) для отрисовки графа
        object locker = new object(); // объект locker для инструкции lock
        Graphics g; // переменная для графики
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            parentTask = "Кнопка"; //событие родитель - кнопка 
            //при каждом нажатии кнопки обнуляем progressBar, а также переменные log и res 
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;
            progressBar4.Value = 0;
            progressBar5.Value = 0;
            progressBar6.Value = 0;
            progressBar7.Value = 0;
            progressBar8.Value = 0;
            progressBar9.Value = 0;
            log = "";
            height = pictureBox1.Height / 2; //делим высоту picturebox на 2, чтобы граф отображался посередине

            //создаем новые потоки с помощью Thread для каждой задачи (функции)
            A = new Thread(
                   () =>
                   {
                       task_A();
                   }
               );

            B = new Thread(
                  () =>
                  {
                      task_B();
                  }
              );
            C = new Thread(
                  () =>
                  {
                      task_C();
                  }
              );
            D = new Thread(
                  () =>
                  {
                      task_D();
                  }
              );
            E = new Thread(
                  () =>
                  {
                      task_E();
                  }
              );
            F = new Thread(
                  () =>
                  {
                      task_F();
                  }
              );
            H = new Thread(
                  () =>
                  {
                      task_H();
                  }
              );
            G = new Thread(
                  () =>
                  {
                      task_G();
                  }
              );
            K = new Thread(
                  () =>
                  {
                      task_K();
                  }
              );

            pictureBox1.CreateGraphics().Clear(Color.White); //делаем фон pictureBox белым
            textBox1.Text = ""; //Очищаем textBox1
            stopwatch.Restart(); //Перезапуск stopwatch
            textBox2.Text = ""; //Очищаем textBox2
            
            //Очищаем массивы и переменные
            f1.Clear();
            f2.Clear();
            f3.Clear();
            f4.Clear();
            f5 = 0;
            f6 = 0;
            f7 = 0;
            f8 = 0;

            try
            {
                //Запускаем процесс
                A.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("процессы еще выполняются");
            }
        }

        void task_A()
        {
            try
            {
                lock (locker)
                {
                    textBox1.Text += $"Поток A начался, инициировал - {parentTask}, время = {time()}\r\n";
                }

                // одномерные массивы длины n
                M1 = new int[n];
                M2 = new int[n];

                for (int i = 0; i < n; i++)
                {
                    //запись рандомных значений в оба массива с использованием random
                    M1[i] = rnd.Next(0, 10);
                    M2[i] = rnd.Next(0, 10);

                    progressBar1.Value = 100 / n * (i + 1); // отрисовываем динамику в progressbar
                    Thread.Sleep(50); // немного усыпляем процесс, чтобы все не слишком быстро отрисовывалось
                }

                lock (locker)
                {
                    pictureBox1.CreateGraphics().DrawLine(pen, new PointF(20, height), new PointF(80, height)); // рисуем линию процесса для графа
                }

                lock (locker)
                {
                    // запись в текстовое поле результатов работы процесса
                    textBox2.Text += "Результат потока А:\n M1 = \r\n";
                    textBox2.Text += " [";

                    for (int i = 0; i < n; i++)
                    {
                        textBox2.Text += M1[i] + ",";
                    }

                    textBox2.Text += "]\r\n";
                    textBox2.Text += "M2 = \r\n";
                    textBox2.Text += " [";

                    for (int i = 0; i < n; i++)
                    {
                        textBox2.Text += M2[i] + ",";
                    }

                    textBox2.Text += "]\r\n";
                }

                lock (locker) {
                    pictureBox1.CreateGraphics().DrawString("A", font, brush, 40, height - 20); // рисуем название процесса на экране (буква A)
                    pictureBox1.CreateGraphics().FillEllipse(brush, 80, height - 2, 4, 4); // рисуем точку в конце линии
                    textBox1.Text += $"Поток А закончился, время = {time()}\r\n"; // записываем в текстовое поле окончание потока 
                }

                lock(locker) { 
                    // указываем задачу - инициатор и запускаем потоки B и C
                    parentTask = "A";
                    textBox1.Text += $"Поток А инициирует поток С\r\n";
                    B.Start();
                    C.Start();
                }
            } catch(Exception ex)
            {
                //в случае ошибок выводим их в текстовое поле и потом записываем в лог 
                textBox2.Text += Convert.ToString(ex) + "\n";
            }
        }

        void task_B()
        {
            try { 
                lock (locker)
                {
                    textBox1.Text += $"Поток B начался, инициировал - {parentTask}, время = {time()}\r\n";
                }

                for (int i = 0; i < n; i++)
                {
                    f1.Add(M1[i]);

                    progressBar2.Value = 100 / n * (i + 1);
                    Thread.Sleep(50);
                }

                lock (locker)
                {
                    pictureBox1.CreateGraphics().DrawLine(pen, new PointF(80, height), new PointF(140, height - 35));
                }

                lock (locker)
                {
                    textBox2.Text += "Результат потока B: f1 = \r\n";
                    textBox2.Text += "[ ";

                    for (int i = 0; i < f1.Count; i++)
                    {
                        textBox2.Text += $"{f1[i]} ";
                    }

                    textBox2.Text += " ]\r\n";
                }

                lock (locker) {
                    pictureBox1.CreateGraphics().DrawString("B", font, brush, 100, height - 35);
                    pictureBox1.CreateGraphics().FillEllipse(brush, 138, height - 37, 4, 4);
                    textBox1.Text += $"Поток B закончился, время = {time()}\r\n";
                }

                lock(locker) { 
                    parentTask = "B";
                    textBox1.Text += $"Поток B инициирует поток D\r\n";
                    D.Start();
                }
            } catch(Exception ex)
            {
                textBox2.Text += Convert.ToString(ex) + "\n";
            }
        }

        void task_C()
        {
            try
            {
                lock (locker)
                {
                    textBox1.Text += $"Поток C начался, инициировал - {parentTask}, время = {time()}\r\n";
                }

                for (int i = 0; i < n; i++)
                {
                    f2.Add(M2[i]);

                    progressBar3.Value = 100 / n * (i + 1);
                    Thread.Sleep(50);
                }

                lock (locker)
                {
                    pictureBox1.CreateGraphics().DrawLine(pen, new PointF(80, height), new PointF(140, height + 35));
                }

                lock (locker)
                {
                    textBox2.Text += "Результат потока C: f2 = \r\n";
                    textBox2.Text += "[ ";

                    for (int i = 0; i < f2.Count; i++)
                    {
                        textBox2.Text += $"{f2[i]} ";
                    }

                    textBox2.Text += " ]\r\n";
                }

                lock (locker) {
                    pictureBox1.CreateGraphics().DrawString("C", font, brush, 100, height + 25);
                    pictureBox1.CreateGraphics().FillEllipse(brush, 138, height + 33, 4, 4);
                    textBox1.Text += $"Поток C закончился, время = {time()}\r\n";
                }

                lock(locker) { 
                    parentTask = "C";
                    textBox1.Text += $"Поток C инициирует поток E\r\n";
                    E.Start();
                }
            } catch(Exception ex)
            {
                textBox2.Text += Convert.ToString(ex) + "\n";
            }
        }

        void task_D()
        {
            try
            {
                lock (locker)
                {
                    textBox1.Text += $"Поток D начался, инициировал - {parentTask}, время = {time()}\r\n";
                }

                f3 = f1;

                for (int i = 0; i < n; i++)
                {
                    f3.Sort();

                    progressBar4.Value = 100 / n * (i + 1);
                    Thread.Sleep(50);
                }

                lock (locker)
                {
                    pictureBox1.CreateGraphics().DrawLine(pen, new PointF(140, height - 35), new PointF(200, height));
                }

                lock (locker)
                {
                    textBox2.Text += "Результат потока D: f3 = \r\n";
                    textBox2.Text += "[ ";

                    for (int i = 0; i < f3.Count; i++)
                    {
                        textBox2.Text += $"{f3[i]} ";
                    }

                    textBox2.Text += " ]\r\n";
                }

                lock (locker) {
                    pictureBox1.CreateGraphics().DrawString("D", font, brush, 170, height - 35);
                    pictureBox1.CreateGraphics().FillEllipse(brush, 198, height - 2, 4, 4);
                    textBox1.Text += $"Поток D закончился, время = {time()}\r\n";
                }

                lock(locker) { 
                    if (!E.IsAlive)
                    {
                        parentTask = "D";
                        textBox1.Text += $"Поток D инициирует потоки F, G, H\r\n";
                        F.Start();
                        G.Start();
                        H.Start();
                    }
                }
            } catch(Exception ex)
            {
                textBox2.Text += Convert.ToString(ex) + "\n";
            }
        }

        void task_E()
        {
            try
            {
                lock (locker)
                {
                    textBox1.Text += $"Поток E начался, инициировал - {parentTask} время = {time()}\r\n";
                }

                f4 = f2;

                for (int i = 0; i < n; i++)
                {
                    f4.Sort();

                    progressBar5.Value = 100 / n * (i + 1);
                    Thread.Sleep(50);
                }

                lock (locker)
                {
                    pictureBox1.CreateGraphics().DrawLine(pen, new PointF(140, height + 35), new PointF(200, height));
                }

                lock (locker)
                {
                    textBox2.Text += "Результат потока E: f4 = \r\n";
                    textBox2.Text += "[ ";

                    for (int i = 0; i < f4.Count; i++)
                    {
                        textBox2.Text += $"{f4[i]} ";
                    }

                    textBox2.Text += " ]\r\n";
                }

                lock (locker) {

                    pictureBox1.CreateGraphics().DrawString("E", font, brush, 170, height + 25);
                    textBox1.Text += $"Поток E закончился, время = {time()}\r\n";
                }

                lock(locker) { 
                    if (!D.IsAlive)
                    {
                        parentTask = "E";
                        textBox1.Text += $"Поток E инициирует потоки F, G, H\r\n";
                        F.Start();
                        G.Start();
                        H.Start();
                    }
                }
            } catch(Exception ex)
            {
                textBox2.Text += Convert.ToString(ex) + "\n";
            }
        }

        void task_F()
        {
            try
            {
                lock (locker)
                {
                    textBox1.Text += $"Поток F начался, инициировал - {parentTask} время = {time()}\r\n";
                }

                for(int i = 0;i < n;i++)
                {
                    f5 += f3[i];
                    f5 += f4[i];

                    progressBar6.Value = 100 / n * (i + 1);
                    Thread.Sleep(50);
                }

                lock (locker)
                {
                    pictureBox1.CreateGraphics().DrawLine(pen, new PointF(200, height), new PointF(260, height - 35));
                    pictureBox1.CreateGraphics().FillEllipse(brush, 258, height - 37, 4, 4);
                    pictureBox1.CreateGraphics().DrawLine(pen, new PointF(260, height - 35), new PointF(320, height));
                }

                lock (locker)
                {
                    textBox2.Text += "Результат потока F: f5 = " + f5 + "\r\n";
                }

                lock (locker)
                {
                    pictureBox1.CreateGraphics().DrawString("F", font, brush, 255, height - 50);
                    textBox1.Text += $"Поток F закончился, время = {time()}\r\n";
                }

                lock (locker)
                {
                    if (!G.IsAlive && !H.IsAlive && !D.IsAlive)
                    {
                        parentTask = "F";
                        textBox1.Text += $"Поток F инициирует поток K\r\n";
                        K.Start();
                    }

                }
            } catch(Exception ex)
            {
                textBox2.Text += Convert.ToString(ex) + "\n";
            }
        }
        void task_G()
        {
            try
            {
                lock (locker)
                {
                    textBox1.Text += $"Поток G начался, инициировал - {parentTask} время = {time()}\r\n";
                }

                int min = 0;

                for (int i = 0; i < n; i++)
                {
                    if (f3[i] < min) min = f3[i];
                    if (f4[i] < min) min = f4[i];

                    progressBar7.Value = 100 / n * (i + 1);
                    Thread.Sleep(50);
                }

                f6 = min;

                lock (locker)
                {
                    pictureBox1.CreateGraphics().DrawLine(pen, new PointF(200, height), new PointF(320, height));
                }

                lock (locker)
                {
                    textBox2.Text += "Результат потока G: f6 = " + f6 + "\r\n";
                }

                lock (locker)
                {
                    pictureBox1.CreateGraphics().DrawString("G", font, brush, 250, height - 20);
                    pictureBox1.CreateGraphics().FillEllipse(brush, 318, height - 2, 4, 4);
                    textBox1.Text += $"Поток G закончился, время = {time()}\r\n";
                }

                lock (locker)
                {
                    if (!F.IsAlive && !H.IsAlive && !D.IsAlive)
                    {
                        parentTask = "G";
                        textBox1.Text += $"Поток G инициирует поток K\r\n";
                        K.Start();
                    }
                }
            } catch(Exception ex)
            {
                textBox2.Text += Convert.ToString(ex) + "\n";
            }
        }
        void task_H()
        {
            try
            {
                lock (locker)
                {
                    textBox1.Text += $"Поток H начался, инициировал - {parentTask} время = {time()}\r\n";
                }

                int max = 0;

                for (int i = 0; i < n; i++)
                {
                    if (f3[i] > max) max = f3[i];
                    if (f4[i] > max) max = f4[i];

                    progressBar8.Value = 100 / n * (i + 1);
                    Thread.Sleep(50);
                }

                f7 = max;

                lock (locker)
                {
                    pictureBox1.CreateGraphics().DrawLine(pen, new PointF(200, height), new PointF(260, height + 35));
                    pictureBox1.CreateGraphics().FillEllipse(brush, 258, height + 33, 4, 4);
                    pictureBox1.CreateGraphics().DrawLine(pen, new PointF(260, height + 35), new PointF(320, height));
                }

                lock (locker)
                {
                    textBox2.Text += "Результат потока H: f7 = " + f7 + "\r\n";
                }

                lock (locker)
                {
                    pictureBox1.CreateGraphics().DrawString("H", font, brush, 255, height + 40);
                    textBox1.Text += $"Поток H закончился, время = {time()}\r\n";
                }

                lock (locker)
                {
                    if (!G.IsAlive && !F.IsAlive && !D.IsAlive)
                    {
                        parentTask = "H";
                        textBox1.Text += $"Поток H инициирует поток K\r\n";
                        K.Start();
                    }
                }
            } catch(Exception ex)
            {
                textBox2.Text += Convert.ToString(ex) + "\n";
            }
        }

        void task_K()
        {
            try
            {
                lock (locker)
                {
                    textBox1.Text += $"Поток K начался, инициировал - {parentTask} время = {time()}\r\n";
                }

                for (int i = 0; i < n; i++)
                {
                    f8 += (f5 + f6 + f7);

                    progressBar9.Value = 100 / n * (i + 1);

                    Thread.Sleep(50);
                }

                f8 /= (3 * n);

                lock (locker)
                {
                    pictureBox1.CreateGraphics().DrawLine(pen, new PointF(320, height), new PointF(380, height));
                    pictureBox1.CreateGraphics().DrawString("K", font, brush, 350, height - 20);
                    pictureBox1.CreateGraphics().FillEllipse(brush, 380, height - 2, 4, 4);
                }

                lock (locker)
                {
                    textBox2.Text += "Результат потока K: f8 = " + f8 + "\r\n";
                }

                lock (locker)
                {
                    textBox1.Text += $"Поток K закончился, время = {time()}\r\n";

                    log = textBox1.Text;
                    log += "\r\n";
                    log += textBox2.Text;

                    string filename = "G:\\systems\\log.txt";
                    File.WriteAllText(filename, log);
                }
            } catch(Exception ex)
            {
                textBox2.Text += Convert.ToString(ex) + "\n";
            }
        }

        string time()
        {
            sec = (int)stopwatch.ElapsedMilliseconds / 1000; // вычисляем секунды
            msec = (int)stopwatch.ElapsedMilliseconds % 1000; // вычисляем миллисекунды

            // если больше или равно 60, то округляем до минут
            if (sec >= 60)
            {
                minutes = sec / 60;
                sec = sec % 60;
            }

            return $"{minutes}:{sec}:{msec}";
        }
    }
}