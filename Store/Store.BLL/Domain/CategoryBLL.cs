using Microsoft.EntityFrameworkCore;
using Store.BLL.Audit;
using Store.Data;
using Store.Models.Audit;
using Store.Models.Domain;
using Store.Models.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Store.BLL.Domain
{
    public class CategoryBLL
    {
        private readonly DataContext _context;
        private readonly ErrorLogBLL _errorLogBLL;
        private readonly ActionLogBLL _actionLogBLL;

        public CategoryBLL(DataContext context, ErrorLogBLL errorLogBLL)
        {
            _context = context;
            _errorLogBLL = errorLogBLL;
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = null;
            try
            {
                categories = _context.Categories
                    .Where(p => p.Status == StatusEnum.ACTIVE)
                    .ToList();
            }
            catch (Exception ex)
            {
                _errorLogBLL.CreateErrorLogEvent(ex, new StackTrace(1, false).GetFrame(0).GetMethod());
            }

            return categories;
        }

        public Category GetCategoryById(int id)
        {
            Category category = null;
            try
            {
                category = _context.Categories
                    .Where(p => p.Status == StatusEnum.ACTIVE)
                    .SingleOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _errorLogBLL.CreateErrorLogEvent(ex, new StackTrace(1, false).GetFrame(0).GetMethod());
            }

            return category;
        }

        public void CreateCategory(Category category)
        {
            var userId = 1;
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                _actionLogBLL.CreateLogEvent(
                    new ActionLog(category.Id, userId, LogTypeEnum.CREATE, EntitiesEnum.CATEGORY)
                );
            }
            catch (Exception ex)
            {
                _errorLogBLL.CreateErrorLogEvent(ex, new StackTrace(1, false).GetFrame(0).GetMethod());
            }
            
        }

        public void UpdateCategory(Category category)
        {
            var userId = 1;
            _context.Entry(category).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                _actionLogBLL.CreateLogEvent(
                    new ActionLog(category.Id, userId, LogTypeEnum.UPDATE, EntitiesEnum.CATEGORY)
                );
            }
            catch (Exception ex)
            {
                _errorLogBLL.CreateErrorLogEvent(ex, new StackTrace(1, false).GetFrame(0).GetMethod());
            }
        }

        public void DeleteCategory(Category category)
        {
            var userId = 1;
            try
            {
                category.SetInactive();
                _context.SaveChanges();
                _actionLogBLL.CreateLogEvent(
                    new ActionLog(category.Id, userId, LogTypeEnum.DELETE, EntitiesEnum.CATEGORY)
                );
            }
            catch (Exception ex)
            {
                _errorLogBLL.CreateErrorLogEvent(ex, new StackTrace(1, false).GetFrame(0).GetMethod());
            }
        }
    }
}
