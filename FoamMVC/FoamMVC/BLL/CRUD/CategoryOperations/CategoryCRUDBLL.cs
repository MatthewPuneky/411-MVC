﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoamMVC.DAL.CRUD.CategoryOperations;
using FoamMVC.Models;
using FoamMVC.ViewModels;

namespace FoamMVC.BLL.CRUD.CategoryOperations
{
    public class CategoryCRUDBLL
    {
        private readonly ICategoryCRUD _categoryCrud;

        public CategoryCRUDBLL()
        {
            _categoryCrud = new CategoryCRUD();     
        }

        #region Gets
        public IList<CategoryViewModel> GetCategoryNameAndID()
        {
            var viewModel = _categoryCrud.Get().Select(x => new CategoryViewModel
            {
                CategoryID = x.ID,
                Name = x.Name
            });

            return viewModel.ToList();
        }

        public List<SelectListItem> GetCategoryForDropDownList()
        {
            return _categoryCrud.Get().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
        } 


        public CategoryDisplayViewModel GetSingleCategoryByIDForDisplay(int id)
        {
            Category DALCategory = _categoryCrud.Get(id);
            return ConvertCategoryToCategoryDisplayViewModel(DALCategory);
        }

        public List<SelectListItem> GetDropDownDisplayForCategory()
        {
           return _categoryCrud.Get().Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.Name
            }).ToList();
        }

        #endregion

        #region Create/Update

        public int CreateCategory(CategoryCreateViewModel viewModel)
        {
            return _categoryCrud.Create(ConvertViewModelToEntity(viewModel));
        }

        public int UpdateCategory(CategoryUpdateViewModel viewModel)
        {
            return _categoryCrud.Update(ConvertViewModelToEntity(viewModel));
        }
        #endregion

        #region Delete/Destroy

        public void DeleteCategory(CategoryViewModel viewModel)
        {
            _categoryCrud.Delete(ConvertViewModelToEntity(viewModel));
        }

        public void DeleteCategories(List<CategoryViewModel> viewModels)
        {
            var items = viewModels.Select(ConvertViewModelToEntity).ToList();
            _categoryCrud.Delete(items);
        }

        public void DestroyCategory(CategoryViewModel viewModel)
        {
            _categoryCrud.Destroy(ConvertViewModelToEntity(viewModel));
        }

        public void DestroyCategories(List<CategoryViewModel> viewModels)
        {
            var items = viewModels.Select(ConvertViewModelToEntity).ToList();
            _categoryCrud.Destroy(items);
        }
        #endregion

        #region Utils

        private Category ConvertViewModelToEntity(CategoryCreateViewModel viewModel)
        {
            return new Category
            {
                Name = viewModel.Name
            };
        }

        private Category ConvertViewModelToEntity(CategoryUpdateViewModel viewModel)
        {
            return new Category
            {
                ID = viewModel.ID,
                Name = viewModel.Name
            };
        }

        private Category ConvertViewModelToEntity(CategoryViewModel viewModel)
        {
            return new Category
            {
                Name = viewModel.Name
            };
        }

        private CategoryDisplayViewModel ConvertCategoryToCategoryDisplayViewModel(Category model)
        {
            return new CategoryDisplayViewModel
            {
                Name = model.Name,
                IsDeleted = model.IsDeleted,
                DateAdded = model.DateAdded,
                DateDeleted = model.DateDeleted,
                DateUpdated = model.DateUpdated
            };
        }
        #endregion
    }
}