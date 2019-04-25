using AutoMapper;
using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.IDataBinding;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DauGiaTrucTuyen.DataBinding
{
    public class CategoryService : ICategory
    {
        Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        //Danh sách danh mục
        public List<ListCategoryViewModel> GetListCategory()
        {
            var list = from category in db.Categorys
                       select new ListCategoryViewModel
                       {
                           Categorys_Id = category.Categorys_Id,
                           CategoryName = category.CategoryName,
                           StatusCategory = category.StatusCategory
                       };
            return list.ToList();
        }

        //Danh sách danh mục cho người dùng
        public List<ListCategoryViewModel> GetListCategoryForClient()
        {
            var list = from category in db.Categorys
                       where category.StatusCategory.Equals(StatusCategory.Opened)
                       select new ListCategoryViewModel
                       {
                           Categorys_Id = category.Categorys_Id,
                           CategoryName = category.CategoryName,
                           StatusCategory = category.StatusCategory
                       };
            return list.ToList();
        }

        //Thêm mới danh mục
        public bool AddCategory(AddCategoryViewModel model)
        {
            var category = Mapper.Map<Data.Category>(model);
            category.Categorys_Id = Guid.NewGuid().ToString();
            category.StatusCategory = StatusCategory.Opened;
            db.Categorys.Add(category);
            db.SaveChanges();
            return true;
        }

        //Sửa danh mục
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

        //Chi tiết danh mục
        public DetailCategoryViewModel DetailCategory(string id)
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