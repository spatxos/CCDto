using System;

namespace CCDto.application.Service.Nav.Dto
{
    [Serializable]
    public class MenusPagedResultRequestDto 
    {
        public virtual string MenuName { get; set; }

        public virtual string Sorting { get; set; }

    }
}
