using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading.Tasks;
using System.Timers;

namespace PiterlandVisitors
{
    public partial class Form1 : Form
    {
        const int COLLUMNS_COUNT = 7;
        const int ROWS_COUNT = 10;

        const int NAME_POSITION = 0;
        const int TIME_CONTROL_POSITION = 1;
        const int START_BUTTON_POSITION = 2;
        const int STOP_BUTTON_POSITION = 3;
        const int PROGRESS_BAR_POSITION = 4;
        const int TIME_LEFT_POSITION = 5;
        const int DELETE_BUTTON_POSITION = 6;

        /// <summary>
        /// Таймер для опроса времени
        /// </summary>
        private System.Timers.Timer timer;

        /// <summary>
        /// Список посетителей
        /// </summary>
        List<Visitor> visitorsList;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            //Настройка таймера
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += timer_Elapsed;

            //Настройка таблицы
            for (int i = 0; i < ROWS_COUNT; i++)
                addRow();

            //Подготовка массива псетителей
            visitorsList = new List<Visitor>();
            for (int i = 0; i < ROWS_COUNT; i++)
            {
                visitorsList.Add(new Visitor());
            }

            timer.Start();
        }

        /// <summary>
        /// Действие при окончании счета таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (InvokeRequired)
                Invoke(
                    new MethodInvoker(() =>
            {
                //Пройтись по всем строкам таблицы и зенести в прогрессбары остатки времени
                ProgressBar pb;
                TextBox tl;
                for (int i = 0; i < ROWS_COUNT; i++)
                {
                    pb = visitorsTableLayoutPanel.Controls[i*COLLUMNS_COUNT + PROGRESS_BAR_POSITION] as ProgressBar;
                    pb.Value = visitorsList[i].getProgress();

                    tl = visitorsTableLayoutPanel.Controls[i * COLLUMNS_COUNT + TIME_LEFT_POSITION] as TextBox;
                    DateTime dt = new DateTime(0);

                    tl.Text = visitorsList[i].getSecondsLeft().ToString();
                }
            }
            ));
        }

        /// <summary>
        /// Добавить строку в таблицу
        /// </summary>
        private void addRow()
        {
            Button startButton = new Button();
            startButton.Text = "Старт";
            startButton.Click += startButton_Click;

            Button stopButton = new Button();
            stopButton.Text = "Стоп";
            stopButton.Click += stopButton_Click;

            ProgressBar progressBar = new ProgressBar();

            Button deleteButton = new Button();
            deleteButton.Text = "Очистить";
            deleteButton.Click += deleteButton_Click;

            NumericUpDown timeControl = new NumericUpDown();
            timeControl.Maximum = 1000;
            timeControl.Minimum = 1;
            timeControl.Value = 60;

            TextBox nameBox = new TextBox();
            nameBox.Text = "";

            TextBox timeLeft = new TextBox();
            timeLeft.ReadOnly = true;
            timeLeft.Text = "00:00";
            
            visitorsTableLayoutPanel.Controls.Add(nameBox, NAME_POSITION, visitorsTableLayoutPanel.RowCount - 1);
            visitorsTableLayoutPanel.Controls.Add(timeControl, TIME_CONTROL_POSITION, visitorsTableLayoutPanel.RowCount - 1);
            visitorsTableLayoutPanel.Controls.Add(startButton, START_BUTTON_POSITION, visitorsTableLayoutPanel.RowCount - 1);
            visitorsTableLayoutPanel.Controls.Add(stopButton, STOP_BUTTON_POSITION, visitorsTableLayoutPanel.RowCount - 1);
            visitorsTableLayoutPanel.Controls.Add(progressBar, PROGRESS_BAR_POSITION, visitorsTableLayoutPanel.RowCount - 1);
            visitorsTableLayoutPanel.Controls.Add(timeLeft, TIME_LEFT_POSITION, visitorsTableLayoutPanel.RowCount - 1);
            visitorsTableLayoutPanel.Controls.Add(deleteButton, DELETE_BUTTON_POSITION, visitorsTableLayoutPanel.RowCount - 1);
            
            visitorsTableLayoutPanel.RowStyles.Add(new RowStyle());
            visitorsTableLayoutPanel.RowCount += 1;
        }

        /// <summary>
        /// Действие при нажатии на кнопку "Очистить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void deleteButton_Click(object sender, EventArgs e)
        {
            TextBox nameBox = visitorsTableLayoutPanel.Controls[((Control)sender).TabIndex - DELETE_BUTTON_POSITION + NAME_POSITION] as TextBox;
            NumericUpDown timeMinutes = visitorsTableLayoutPanel.Controls[((Control)sender).TabIndex - DELETE_BUTTON_POSITION + TIME_CONTROL_POSITION] as NumericUpDown;
            ProgressBar pb = visitorsTableLayoutPanel.Controls[((Control)sender).TabIndex - DELETE_BUTTON_POSITION + PROGRESS_BAR_POSITION] as ProgressBar;
            Button stopButton = visitorsTableLayoutPanel.Controls[((Control)sender).TabIndex - DELETE_BUTTON_POSITION + STOP_BUTTON_POSITION] as Button;
            Button startButton = visitorsTableLayoutPanel.Controls[((Control)sender).TabIndex - DELETE_BUTTON_POSITION + START_BUTTON_POSITION] as Button;

            int index = ((Control)sender).TabIndex / COLLUMNS_COUNT;
            visitorsList[index] = new Visitor();

            nameBox.ReadOnly = false;
            nameBox.Text = "";

            timeMinutes.ReadOnly = false;
            timeMinutes.Value = 60;

            pb.Value = 0;

            stopButton.BackColor = Color.FromName("White");

            startButton.BackColor = Color.FromName("White");
        }

        /// <summary>
        /// Действие при нажатии на кнопку "Стоп внутри таблицы"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void stopButton_Click(object sender, EventArgs e)
        {
            //Получить ссылку на поле с именем
            TextBox nameBox = visitorsTableLayoutPanel.Controls[((Control)sender).TabIndex - STOP_BUTTON_POSITION + NAME_POSITION] as TextBox;

            if (!nameBox.ReadOnly)
                return;

            //Получить имя посетителя
            string name = nameBox.Text;

            //Задать цвета кнопкам старт и стоп
            (sender as Button).BackColor = Color.FromName("Coral");

            Button startButton = visitorsTableLayoutPanel.Controls[((Control)sender).TabIndex - STOP_BUTTON_POSITION + START_BUTTON_POSITION] as Button;
            startButton.BackColor = Color.FromName("White");

            //Получить индекс посетителя
            int visitorIndex = ((Control)sender).TabIndex / COLLUMNS_COUNT;

            //Запустить счет для посетителя
            visitorsList[visitorIndex].stop();
        }

        /// <summary>
        /// Действие при нажатии на кнопку "Старт внутри таблицы"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void startButton_Click(object sender, EventArgs e)
        {
            //Получить ссылку на поле с именем
            TextBox nameBox = visitorsTableLayoutPanel.Controls[((Control)sender).TabIndex - START_BUTTON_POSITION + NAME_POSITION] as TextBox;
            NumericUpDown timeMinutes = visitorsTableLayoutPanel.Controls[((Control)sender).TabIndex - START_BUTTON_POSITION + TIME_CONTROL_POSITION] as NumericUpDown;

            //Получить имя посетителя
            string name = nameBox.Text;

            if (name == "")
            {
                MessageBox.Show("Введите имя!");
                return;
            }

            int time_in_seconds = (int)(timeMinutes.Value * 60);

            //Получить индекс посетителя
            int visitorIndex = ((Control)sender).TabIndex / COLLUMNS_COUNT;

            //Инициалзировать, если надо
            if (!nameBox.ReadOnly)
            {
                visitorsList[visitorIndex] = new Visitor(name, time_in_seconds);
            }

            //Сделать некоторые контролы только для чтения
            nameBox.ReadOnly = true;
            timeMinutes.ReadOnly = true;

            //Задать цвета кнопкам старт и стоп
            (sender as Button).BackColor = Color.FromName("Coral");

            Button stopButton = visitorsTableLayoutPanel.Controls[((Control)sender).TabIndex - START_BUTTON_POSITION + STOP_BUTTON_POSITION] as Button;
            stopButton.BackColor = Color.FromName("White");

            //Запустить счет для посетителя
            visitorsList[visitorIndex].start();
        }
    }
}
