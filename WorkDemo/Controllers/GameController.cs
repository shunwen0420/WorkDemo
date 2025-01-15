using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkDemo.Context;
using WorkDemo.Models;

namespace WorkDemo.Controllers
{
    public class GameController(GameContext _context) : Controller
    {

        /// <summary>
        /// 取得遊戲清單
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Games.ToListAsync());
        }

        /// <summary>
        /// 取得遊戲明細
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var result = await _context.Games.FindAsync(id);
            return result is null ? NotFound() : View(result);
        }

        /// <summary>
        /// 新增遊戲_檢視
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 新增遊戲_動作
        /// </summary>
        /// <param name="gameViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameID,GameName,Image,Price")] GameViewModel gameViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameViewModel);
        }

        /// <summary>
        /// 編輯遊戲_檢視
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var gameViewModel = await _context.Games.FindAsync(id);
            if (gameViewModel is null)
            {
                return NotFound();
            }
            return View(gameViewModel);
        }

        /// <summary>
        /// 編輯遊戲_動作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gameViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameID,GameName,Image,Price")] GameViewModel gameViewModel)
        {
            if (id != gameViewModel.GameID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameViewModelExists(gameViewModel.GameID))
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
            return View(gameViewModel);
        }

        /// <summary>
        /// 刪除遊戲_檢視
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameViewModel = await _context.Games.FindAsync(id);
            return gameViewModel == null ? NotFound() : View(gameViewModel);
        }

        /// <summary>
        /// 刪除遊戲_動作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameViewModel = await _context.Games.FindAsync(id);
            if (gameViewModel != null)
            {
                _context.Games.Remove(gameViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameViewModelExists(int id)
        {
            return _context.Games.Any(e => e.GameID == id);
        }
    }
}
