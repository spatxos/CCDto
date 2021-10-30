using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CCDto.application.Service.Crud.Dto
{
    /// <summary>
    /// Simply implements <see cref="ILimitedResultRequest"/>.
    /// </summary>
    public class LimitedResultRequestDto : ILimitedResultRequest
    {
        [Range(1, int.MaxValue)]
        public virtual int PageSize { get; set; } = 10;
    }
}
