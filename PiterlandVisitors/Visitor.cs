using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiterlandVisitors
{
    /// <summary>
    /// Класс для описания посетителя
    /// </summary>
    class Visitor
    {
        #region Поля

        /// <summary>
        /// Время последнего обновления таймера
        /// </summary>
        DateTime last_invalidation_time;

        /// <summary>
        /// Имя посетителя
        /// </summary>
        string name;

        /// <summary>
        /// Оставшееся время для посещения
        /// </summary>
        int seconds_left;

        /// <summary>
        /// Общее время посещения (для расчета в процентах)
        /// </summary>
        int all_time;

        /// <summary>
        /// Флаг того, что идет счет
        /// </summary>
        bool timer_started;

        #endregion

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public Visitor(string name = "N/A", int time = 0) 
        {
            timer_started = false;
            this.name = name;
            this.all_time= this.seconds_left = time;
        }

        #region Методы

        public bool isStarted()
        {
            return timer_started;
        }

        /// <summary>
        /// Получить имя посетителя
        /// </summary>
        /// <returns>имя посетителя</returns>
        public string getName()
        {
            return name;
        }

        /// <summary>
        /// Запросить оставшееся время в процентах
        /// </summary>
        /// <returns>Сколько осталось от общего времени</returns>
        public int getProgress() 
        {
            invalidate_time();

            if (seconds_left == 0) 
                return 0;

            return (int)(((double)seconds_left) / all_time * 100);
        }

        /// <summary>
        /// Запросить оставшееся время в секундах
        /// </summary>
        /// <returns> Оставшееся время в секундах</returns>
        public int getSecondsLeft() 
        {
            invalidate_time();
            return seconds_left;
        }

        /// <summary>
        /// Начать счет
        /// </summary>
        public void start() 
        {
            last_invalidation_time = DateTime.Now;
            timer_started = true;
        }

        /// <summary>
        /// Завершить счет
        /// </summary>
        public void stop() 
        {
            timer_started = false;
        }

        /// <summary>
        /// Обновить значение оставшегося времени
        /// </summary>
        private void invalidate_time()
        {
            if (timer_started)
            {
                seconds_left -= (DateTime.Now - last_invalidation_time).Seconds;
            }

            if (seconds_left < 0)
            {
                seconds_left = 0;
            }

            last_invalidation_time = DateTime.Now;
        }

        #endregion
    }
}
