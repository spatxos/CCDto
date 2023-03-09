using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.DynamicWebApi;
using Panda.DynamicWebApi.Attributes;

namespace ModelLibrary
{
    //[DynamicWebApi]
    //public interface IApplicationService : IDynamicWebApi
    //{

    //}

    [Service("Apple.Server")]
    public class AppleAppService //: IApplicationService
    {
        private static readonly Dictionary<int, string> Apples = new Dictionary<int, string>()
        {
            [1] = "Big Apple",
            [2] = "Small Apple"
        };

        [AllowAnonymous]
        public async Task UpdateAppleAsync(dynamic dto)
        {
            await Task.Run(() => {
                if (Apples.ContainsKey(dto.Id))
                {
                    Apples[dto.Id] = dto.Name;
                }
            });

        }

        /// <summary>
        /// Get An Apple.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public string Get(int id)
        {
            if (Apples.ContainsKey(id))
            {
                return Apples[id];
            }
            else
            {
                return "No Apple!";
            }
        }

        /// <summary>
        /// Get  All Apple Async.
        /// </summary>
        /// <returns></returns>
        //[NonDynamicMethod]
        public IEnumerable<string> GetAllAsync()
        {
            return Apples.Values;
        }

        /// <summary>
        /// Get All Apple.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return Apples.Values;
        }

        /// <summary>
        /// Get All Apple.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetBigApple()
        {
            return Apples.Values;
        }

        /// <summary>
        /// Update Apple
        /// </summary>
        /// <param name="dto"></param>
        public void Update(dynamic dto)
        {
            if (Apples.ContainsKey(dto.Id))
            {
                Apples[dto.Id] = dto.Name;
            }
        }

        /// <summary>
        /// Delete Apple
        /// </summary>
        /// <param name="id">Apple Id</param>
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            if (Apples.ContainsKey(id))
            {
                Apples.Remove(id);
            }
        }

    }
}