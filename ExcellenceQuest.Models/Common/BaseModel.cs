namespace ExcellenceQuest.Models.Common

{
    using System;

    public  class BaseObject
    {
        public  int? Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }
    }
}
