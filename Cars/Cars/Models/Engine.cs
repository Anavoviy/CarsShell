using Cars.Services;
using Cars.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cars.Models
{
    public class Engine
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Model { get; set; }
        public double CylinderCapacity { get; set; }
        public string CylinderArrangement { get; set; }
        public int HorsePower { get; set; }

        public override string ToString()
        {
            return Model + " Компоновка: " + CylinderArrangement + " Объём: " + CylinderCapacity + "л." + " Мощность: " + HorsePower + "л.с.";
        }

        public BaseCommandParameter DeleteEngine { get; set; } = new BaseCommandParameter(async (arg) =>
        {
            MainViewModel.DeleteEngine.Execute(arg);
        });
        public BaseCommandParameter EditEngine { get; set; } = new BaseCommandParameter(async (arg) =>
        {
            await Shell.Current.GoToAsync($"/EditEngine?id={((Engine)arg).Id}");
        });
    }
}
