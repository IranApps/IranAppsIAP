
using System.Collections.Generic;
using SimpleJSON;

//#if UNITY_ANDROID

namespace IranAppsPlugin
{
    public class IranAppsSkuInfo
    {
        public string Title { get; private set; }
        public string Price { get; private set; }
        public string Type { get; private set; }
        public string Description { get; private set; }
        public string ProductId { get; private set; }

        public static List<IranAppsSkuInfo> fromJsonArray(JSONArray items)
        {
            var skuInfos = new List<IranAppsSkuInfo>();

            foreach (JSONNode item in items.AsArray)
            {
                IranAppsSkuInfo bSkuInfo = new IranAppsSkuInfo();
                bSkuInfo.fromJson(item.AsObject);
                skuInfos.Add(bSkuInfo);
            }

            return skuInfos;
        }

        public IranAppsSkuInfo() { }

        public void fromJson(JSONClass json)
        {
            Title = json["title"].Value;
            Price = json["price"].Value;
            Type = json["type"].Value;
            Description = json["description"].Value;
            ProductId = json["productId"].Value;
        }

        public override string ToString()
        {
            return string.Format("<IranAppsSkuInfo> title: {0}, price: {1}, type: {2}, description: {3}, productId: {4}",
                Title, Price, Type, Description, ProductId);
        }

    }
}
//#endif