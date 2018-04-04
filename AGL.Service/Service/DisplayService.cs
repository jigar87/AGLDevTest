using AGL.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AGL.Service.Service
{
    public class DisplayService
    {
        //HttpClient is intended to be instantiated once for life of an application.
        private static readonly HttpClient _client = new HttpClient();
        public async Task<string> GetAPIData(string apiUrl)
        {
            try
            {
                using (var response = await _client.GetAsync(new Uri(apiUrl)))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            catch(Exception ex)
            {
                //TODO: Add log4net error logging.
                throw ex;
            }
        }

        private List<Owner> GetOwnerAndPetData(string apiUrl)
        {
            var apiData = GetAPIData(apiUrl).Result;
            var ownerList = JsonConvert.DeserializeObject<List<Owner>>(apiData);

            return ownerList;
        }

        /// <summary>
        /// Verifies whether the pet owners gender is same as passed in the parameter.
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="petName"></param>
        /// <param name="ownersGender"></param>
        /// <returns></returns>
        public bool VerifyOwnersGender(string apiUrl, string petName, string ownersGender)
        {
            var ownerList = GetOwnerAndPetData(apiUrl);
            var petOwnerExists = (from o in ownerList
                                   where o.Pets != null &&
                                   o.Gender == ownersGender &&
                                   o.Pets.Any(p => p.Name == petName)
                                   select o).Any();

            return petOwnerExists;

        }


        /// <summary>
        /// Fetches the owner and pet data from the API, groups by pet owners gender for pet type Cat
        /// and within each group orders cat names in ascending order.
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        public List<Cats> GetCatsGroupedByOwnerGender(string apiUrl)
        {
            try
            {
                var ownerList = GetOwnerAndPetData(apiUrl);
                var query = (from owner in ownerList
                             where owner.Pets != null
                             group owner by new { owner.Gender } into grp
                             select new Cats
                             {
                                 OwnersGender = grp.Key.Gender,
                                 CatNames = (grp.SelectMany(ow => ow.Pets).Where(p => p.Type == "Cat").
                                            OrderBy(x => x.Name).
                                            Select(y => y.Name)).ToList()
                             }).ToList();

                return query;
            }
            catch(Exception ex)
            {
                //TODO: Add log4net error logging.
                throw ex;
            }

        }

    }
}
