using AutoMapper;
using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DauGiaTrucTuyen.DataBinding
{
    public class Category
    {
        Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();
        public List<ListCategoryViewModel> GetListCategory()
        {
            var list = from category in db.Categorys
                       select new ListCategoryViewModel
                       {
                           Categorys_Id = category.Categorys_Id,
                           CategoryName = category.CategoryName
                       };
            return list.ToList();
        }
        public bool AddCategory(AddCategoryViewModel model)
        {
            var category = Mapper.Map<Data.Category>(model);
            category.Categorys_Id = Guid.NewGuid().ToString();
            db.Categorys.Add(category);
            db.SaveChanges();
            return true;
        }
        public bool EditCategory(EditCategoryViewModel model)
        {
            var category = Mapper.Map<Data.Category>(model);
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        public bool DeleteCategory(string id)
        {
            var category = db.Categorys.Find(id);
            if (category != null)
            {
                db.Categorys.Remove(category);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public DetailCategoryViewModel DetailUser(string id)
        {
            var category = db.Categorys.Find(id);
            if (category != null)
            {
                var model = Mapper.Map<DetailCategoryViewModel>(category);
                return model;
            }
            return null;
        }
    }
}