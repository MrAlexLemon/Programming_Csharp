using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    /// <summary>         
    /// Клас проект         
    /// </summary>         

    public class Project
    {
        /// <summary>             
        /// Шифр
        /// </summary> 
        public int id;

        /// <summary>             
        /// Назва проекта
        /// </summary>             
        public string name;

        /// <summary>             
        /// Ціна проекта             
        /// </summary>            
        public double value;

        /// <summary>
        /// Дата початку проекта
        /// </summary>
        public DateTime startDate;

        /// <summary>
        /// Дата кінця проекта
        /// </summary>
        public DateTime finishDate;

        /// <summary>
        /// Список учасників проекта
        /// </summary>
        public List<string> listName;

        /// <summary>             
        /// Конструктор             
        /// </summary>             
        public Project(int i, string g, double v, DateTime sd, DateTime fd, List<string> ln)
        {
            this.id = i;
            this.name = g;
            this.value = v;
            this.startDate = sd;
            this.finishDate = fd;
            this.listName = ln;
        }

        /// <summary>            
        /// Переоприділення метода(Вивести інформацію про проект)       
        /// </summary>     
        public override string ToString()
        {
            return "(id=" + this.id.ToString() + "; name=" + this.name + "; value=" + this.value + "; startDate=" + this.startDate + "; finishDate=" + finishDate + ")";
        }
    }
