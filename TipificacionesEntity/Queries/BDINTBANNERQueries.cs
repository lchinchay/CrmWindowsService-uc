using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Linq;
using TipificacionesEntity.Contexts;
using TipificacionesEntity.Maps.HLPCNTR;

namespace TipificacionesEntity.Queries
{

    public class BDINTBANNERQueries
    {
        private readonly BDINTBANNERContext _context;

        public BDINTBANNERQueries(DbContextOptions<BDINTBANNERContext> options)
        {
            _context = new BDINTBANNERContext(options);
        }

        public void setDataTicketTopic(string codigoTemaCRM, string nombre, string descripcion, bool status)
        {
            try
            {
                var ticketTopic = _context.TicketTopic
                    .Where(w => w.CodigoTemaCRM == codigoTemaCRM)
                    .FirstOrDefault();

                if (ticketTopic != null)
                {
                    ticketTopic.Status = status ? 1 : 0;
                    ticketTopic.Description = descripcion;
                    ticketTopic.Name = nombre;
                }
                else
                {
                    TicketTopicTable newTopic = new TicketTopicTable();
                    newTopic.CodigoTemaCRM = codigoTemaCRM;
                    newTopic.Status = status ? 1 : 0;
                    newTopic.Description = descripcion;
                    newTopic.Name = nombre;
                    _context.TicketTopic.Add(newTopic);
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                if (e.InnerException != null) Log.Error("INNER EXCEPTION: " + e.InnerException.Message);
            }
        }

        public void setDataTicketCategory(int? canalId, string codigoCategoriaCRM, string nombreCategoria, string descripcionCategoria, bool statusCategoria,
            string codigoSubcategoriaCRM, string nombreSubcategoria, string descripcionSubcategoria, bool statusSubcategoria)
        {
            try
            {
                var ticketCategory = _context.TicketCategory
                    .Where(w => w.CodigoCategoriaCRM == codigoCategoriaCRM && w.CanalId == canalId)
                    .FirstOrDefault();

                int categoryId;
                if (ticketCategory != null)
                {
                    ticketCategory.Status = statusCategoria ? 1 : 0;
                    ticketCategory.Description = descripcionCategoria;
                    ticketCategory.Name = nombreCategoria;
                    ticketCategory.CanalId = canalId;

                    categoryId = ticketCategory.TicketCategoryID;
                }
                else
                {
                    TicketCategoryTable newCategory = new TicketCategoryTable();
                    newCategory.CodigoCategoriaCRM = codigoCategoriaCRM;
                    newCategory.Status = statusCategoria ? 1 : 0;
                    newCategory.Description = descripcionCategoria;
                    newCategory.Name = nombreCategoria;
                    newCategory.CanalId = canalId;

                    _context.TicketCategory.Add(newCategory);
                    _context.SaveChanges();

                    categoryId = newCategory.TicketCategoryID;
                }

                var ticketSubcategory = _context.TicketCategory
                    .Where(w => w.CodigoCategoriaCRM == codigoSubcategoriaCRM && w.CanalId == canalId)
                    .FirstOrDefault();

                if (ticketSubcategory != null)
                {
                    ticketSubcategory.Status = statusSubcategoria ? 1 : 0;
                    ticketSubcategory.Description = descripcionSubcategoria;
                    ticketSubcategory.Name = nombreSubcategoria;
                    ticketSubcategory.ParentCategoryID = categoryId;

                    ticketSubcategory.CanalId = canalId;
                }
                else
                {
                    TicketCategoryTable newSubcategory = new TicketCategoryTable();
                    newSubcategory.CodigoCategoriaCRM = codigoSubcategoriaCRM;
                    newSubcategory.Status = statusSubcategoria ? 1 : 0;
                    newSubcategory.Description = descripcionSubcategoria;
                    newSubcategory.Name = nombreSubcategoria;
                    newSubcategory.ParentCategoryID = categoryId;
                    newSubcategory.CanalId = canalId;

                    _context.TicketCategory.Add(newSubcategory);
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                if (e.InnerException != null) Log.Error("INNER EXCEPTION: " + e.InnerException.Message);
            }
        }

        public void setDataTicketType(int? canalId, string codigoAtencionCRM, string nombreAtencion, string descripcionAtencion, bool statusAtencion)
        {
            try
            {
                var ticketType = _context.TicketType
                    .Where(w => w.CodigoTipoAtencionCRM == codigoAtencionCRM && w.CanalId == canalId)
                    .FirstOrDefault();

                if (ticketType != null)
                {
                    ticketType.Status = statusAtencion ? 1 : 0;
                    ticketType.Description = descripcionAtencion;
                    ticketType.Name = nombreAtencion;
                    ticketType.TicketSourceID = 1;
                    ticketType.CanalId = canalId;
                }
                else
                {
                    TicketTypeTable newType = new TicketTypeTable();
                    newType.CodigoTipoAtencionCRM = codigoAtencionCRM;
                    newType.Status = statusAtencion ? 1 : 0;
                    newType.Description = descripcionAtencion;
                    newType.Name = nombreAtencion;
                    newType.TicketSourceID = 1;
                    newType.CanalId = canalId;

                    _context.TicketType.Add(newType);
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                if (e.InnerException != null) Log.Error("INNER EXCEPTION: " + e.InnerException.Message);
            }
        }

        public int? setDataTicketCanal(string codigoCanalCRM, string descripcionCanal)
        {
            try
            {
                var ticketCanal = _context.TicketCanal
                    .Where(w => w.Codigo == codigoCanalCRM)
                    .FirstOrDefault();

                int canalId;

                if (ticketCanal != null)
                {
                    ticketCanal.Description = descripcionCanal;
                    ticketCanal.Status = 1;

                    if (ticketCanal.Codigo == "002")
                    {
                        ticketCanal.FlagPortal = true;
                    }

                    canalId = ticketCanal.CanalId;

                    _context.SaveChanges();
                }
                else
                {
                    TicketCanalTable newCanal = new TicketCanalTable();
                    newCanal.Codigo = codigoCanalCRM;
                    newCanal.Description = descripcionCanal;
                    newCanal.Status = 1;

                    _context.TicketCanal.Add(newCanal);
                    _context.SaveChanges();

                    canalId = newCanal.CanalId;
                }

                return canalId;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                if (e.InnerException != null) Log.Error("INNER EXCEPTION: " + e.InnerException.Message);
            }

            return null;
        }
    }
}
