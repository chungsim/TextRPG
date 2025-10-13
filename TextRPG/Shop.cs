using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Shop
    {

    }

    public class ShopPreset 
    {
        public static List<List<Item>> shopItemList = new List<List<Item>>() 
        { 
            new List<Item>(){ItemPreset.itemList[0],ItemPreset.itemList[1],ItemPreset.itemList[8]},
            new List<Item>(){ItemPreset.itemList[4],ItemPreset.itemList[8],ItemPreset.itemList[8]},
            new List<Item>(){ItemPreset.itemList[8],ItemPreset.itemList[8],ItemPreset.itemList[0]},
            new List<Item>(){ItemPreset.itemList[2],ItemPreset.itemList[8],ItemPreset.itemList[9]},
            new List<Item>(){ItemPreset.itemList[0],ItemPreset.itemList[0],ItemPreset.itemList[0]},
            new List<Item>(){ItemPreset.itemList[0],ItemPreset.itemList[0],ItemPreset.itemList[0]},
            new List<Item>(){ItemPreset.itemList[0],ItemPreset.itemList[0],ItemPreset.itemList[0]},
            new List<Item>(){ItemPreset.itemList[0],ItemPreset.itemList[0],ItemPreset.itemList[0]},
            new List<Item>(){ItemPreset.itemList[0],ItemPreset.itemList[0],ItemPreset.itemList[0]},
            new List<Item>(){ItemPreset.itemList[10],ItemPreset.itemList[10],ItemPreset.itemList[11]},
        };

        
    }
}
