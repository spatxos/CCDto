using System.Collections.Generic;

namespace CCDto.application.Service.Nav.Dto
{

    public class MenuDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Order { get; set; }
        public List<MenuDto> Childs { get; set; }
    }
}
