using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace parse_yelp
{
    
    /* The list of the main categories. All other categories that appear in the business objects are considered sub-categories. */
    class Categories
    {
        public String[] mainCategories = new String[28];  
        public Categories()
        {

            mainCategories[0] =  "Active Life";
            mainCategories[1] =  "Arts & Entertainment";    
            mainCategories[2] =  "Automotive";
            mainCategories[3] =  "Car Rental";
            mainCategories[4] =  "Cafes";
            mainCategories[5] =  "Beauty & Spas";
            mainCategories[6] =  "Convenience Stores";
            mainCategories[7] =  "Dentists";
            mainCategories[8] =  "Doctors";
            mainCategories[9] =  "Drugstores";
            mainCategories[10] =  "Department Stores";
            mainCategories[11] =  "Education";
            mainCategories[12] =  "Event Planning & Services";
            mainCategories[13] =  "Flowers & Gifts";
            mainCategories[14] =  "Food";
            mainCategories[15] =  "Health & Medical";
            mainCategories[16] =  "Home Services";
            mainCategories[17] =  "Home & Garden";
            mainCategories[18] =  "Hospitals";
            mainCategories[19] =  "Hotels & Travel";
            mainCategories[20] =  "Hardware Stores";
            mainCategories[21] =  "Grocery";
            mainCategories[22] =  "Medical Centers";
            mainCategories[23] =  "Nurseries & Gardening";
            mainCategories[24] =  "Nightlife";
            mainCategories[25] =  "Restaurants";
            mainCategories[26] =  "Shopping";
            mainCategories[27] =  "Transportation";
        }

        public int cLength()
        {
            return mainCategories.Length;
        }

        public bool cExists(string bCategory)
        {
            return mainCategories.Contains(bCategory);
                                  
        }
    }
}
