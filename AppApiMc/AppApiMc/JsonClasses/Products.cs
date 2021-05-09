using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppApiMc.JsonClasses
{
    class Products
    {
        public int lastUpdated { get; set; }
        public itemsProduct[] items { get; set; }
    }

    public class itemsProduct
    {
        public int productId { get; set; }
        public string size { get; set; }
        public string snippetDescription { get; set; }
        public string name { get; set; }
        public string[] ingredients { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public bool deleted { get; set; }
        public double energyKcal { get; set; }
        public double energyKj { get; set; }
        public ImagesAndroid imagesAndroid { get; set; }
        public ImagesAndroidLarge imagesAndroidLarge { get; set; }
        public ImagesIos imagesIos { get; set; }
        public ImagesIosLarge imagesIosLarge { get; set; }
        public MealChoices[] mealChoices { get; set; } // исправить
        public Nutritious nutritious { get; set; }
        public int optionalMenu { get; set; }
        public int sorting { get; set; }
        public int tag { get; set; }
        public double weight { get; set; }
        public string[] allergens { get; set; }
    }
    public class ImagesAndroid
    {
        public string l { get; set; }
        public string m { get; set; }
        public string s { get; set; }
    }
    public class ImagesAndroidLarge
    {
        public string l { get; set; }
        public string m { get; set; }
        public string s { get; set; }
    }
    public class ImagesIos
    {
        public string l { get; set; }
        public string m { get; set; }
        public string s { get; set; }
    }
    public class ImagesIosLarge
    {
        public string l { get; set; }
        public string m { get; set; }
        public string s { get; set; }
    }
    public class Nutritious
    {
        public double fatGrams { get; set; }
        public double fatRi { get; set; }
        public double carbohydrateGrams { get; set; }
        public double carbohydrateRi { get; set; }
        public double proteinGrams { get; set; }
        public double proteinRi { get; set; }
        public string snippetDescription { get; set; }
    }
    public class MealChoices
    {
        public string title { get; set; }
        public Choices[] choices { get; set; }
        public int sorting { get; set; }
    }
    public class Choices
    {
        public string id { get; set; }
        public bool hidden { get; set; }
        public int sorting { get; set; }
    }
}
