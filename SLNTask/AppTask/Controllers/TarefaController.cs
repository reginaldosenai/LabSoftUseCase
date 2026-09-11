using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppTask.Models;

namespace AppTask.Controllers
{
    public class TarefaController : Controller
    {
        private readonly DbTasksContext _context;
        private RegraTarefa _regraTarefa;

        public TarefaController(DbTasksContext context)
        {
            _context = context;
            _regraTarefa = new RegraTarefa();
        }

        public async Task<IActionResult> Index()
        {
            var dbTasksContext = _context.Tarefas.Include(t => t.Funcionario);
            return View(await dbTasksContext.ToListAsync());
        }

        public async Task<IActionResult> Sobre()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .Include(t => t.Funcionario)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        public IActionResult Create()
        {
            ViewData["ListaFuncionario"] = new SelectList(_context.Funcionarios, "Codigo", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Descricao,DataPlanejada,DataIniciada,DataFinalizada,DataCancelada,StatusTarefa,Prazo,FuncionarioId")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListaFuncionario"] = new SelectList(_context.Funcionarios, "Codigo", "Nome", tarefa.FuncionarioId);
            return View(tarefa);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Codigo", "Nome", tarefa.FuncionarioId);
            return View(tarefa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Descricao,DataPlanejada,DataIniciada,DataFinalizada,DataCancelada,StatusTarefa,Prazo,FuncionarioId")] Tarefa tarefa)
        {
            if (id != tarefa.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid && _regraTarefa.validarDataFinal(tarefa.DataIniciada, tarefa.DataFinalizada))
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(tarefa.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Codigo", "Nome", tarefa.FuncionarioId);
            return View(tarefa);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .Include(t => t.Funcionario)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefas.Any(e => e.Codigo == id);
        }
    }

    public class RegraTarefa
    {
        public bool validarDataFinal(DateTime? dataIniciada, DateTime? dataFinalizada)
        {
            if (dataIniciada.HasValue && dataFinalizada.HasValue)
            {
                return dataFinalizada >= dataIniciada;
            }
            return true;
        }
    }
}