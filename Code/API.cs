using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;



namespace Mod
{
    //thanks to pjstarr12 for making this!
    public struct ChemistryAPI 
    { 
        public static void AddLiquidToItem(string ExistingItem, string NewLiquidID, float LiquidAmount) 
        { 
            ModAPI.FindSpawnable(ExistingItem).Prefab.AddComponent<FlaskBehaviour>();
            ModAPI.FindSpawnable(ExistingItem).Prefab.GetComponent<FlaskBehaviour>().StartLiquid = new BloodContainer.SerialisableDistribution 
            { 
                LiquidID = NewLiquidID,
                Amount = LiquidAmount
            };
        }
        //ChemistryAPI.AddLiquidToItem("Rotor","OIL",0.28f);

        public static void LiquidReaction(string FirstLiquid, string SecondLiquid, string TargetLiquid) 
        { 
            var mixer = new LiquidMixInstructions(
                Liquid.GetLiquid(FirstLiquid), 
                Liquid.GetLiquid(SecondLiquid),                         
                Liquid.GetLiquid(TargetLiquid));                   

            LiquidMixingController.MixInstructions.Add(mixer);  
        }
        //ChemistryAPI.LiquidReaction("LIFE SERUM","TRITIUM","INSTANT DEATH POISON");

        public static void TripleLiquidReaction(string FirstLiquid, string SecondLiquid, string ThirdLiquid, string TargetLiquid) 
        { 
            var mixer = new LiquidMixInstructions(
                Liquid.GetLiquid(FirstLiquid), 
                Liquid.GetLiquid(SecondLiquid),
                Liquid.GetLiquid(ThirdLiquid),                         
                Liquid.GetLiquid(TargetLiquid));                   

            LiquidMixingController.MixInstructions.Add(mixer);  
        }
        //ChemistryAPI.LiquidReaction("SUGAR","SPICE","EVERYTHING NICE","THE PERFECT LITTLE GIRL");
    }

    //originally sto- er, borrowed from human tiers mod
    public class CategoryBuilder
    {
        public static void Create(string name,string description, Sprite icon)
        {
            CatalogBehaviour manager = UnityEngine.Object.FindObjectOfType<CatalogBehaviour>();
            if (manager.Catalog.Categories.FirstOrDefault((Category c) => c.name == name) == null)
            {
                Category category = ScriptableObject.CreateInstance<Category>();
                category.name = name;
                category.Description = description;
                category.Icon = icon;
                Category[] NewCategories = new Category[manager.Catalog.Categories.Length + 1];
                Category[] categories = manager.Catalog.Categories;
                for (int i = 0; i < categories.Length; i++)
                {
                    NewCategories[i] = categories[i];
                }
                NewCategories[NewCategories.Length - 1] = category;
                manager.Catalog.Categories = NewCategories;
            }
        }   
    }
}

