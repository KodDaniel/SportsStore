using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SportsStore.Infrastructure
{
    public static class SessionExtensions
    {
        // Metoden tar ett objekt och string-nyckel
        // Och sparar objektet i en session med denna nyckel
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // En extension method som försöker hämta ut en session utefter en given string-nyckel
        public static T GetJson<T>(this ISession session, string key)
        {
            // Försöker hämta ut en session utifrån en nyckel (i detta fall "Cart")
            // Om det existerar en sådan session kommer den sparas i variabeln sessionData
            // Annars kommer variabeln sessionData bli null
            var sessionData = session.GetString(key);
            
            //Om sessionData är null returnerar vi default(T). Kom ihåg att Default(T) för
            //en referenstyp alltid blir null. Om sessionData inte är null
            // vill vi deserialisera objektet (från JSON Till Cart) och returnera det
            // till metoden GetCart()

           return  (sessionData == null) ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);

        }
    }
}
