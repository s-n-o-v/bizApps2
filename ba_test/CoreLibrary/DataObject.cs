namespace CoreLibrary
{
    using System.Collections.Generic;

    public class DataObject
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }

        public IList<UserObject> data { get; set; }
    }
}
