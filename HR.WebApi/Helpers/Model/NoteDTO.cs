using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Helpers.Model
{
    public class NoteDTO
    {
        public Int32 Id { get; set; }

        public string Title { get; set; }

        public string Desc { get; set; }

        public Int32 ContactId { get; set; }
    }
}