using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.IO
{
    public sealed class Menu
    {
        public delegate void MenuMethod();

        public List<MenuMethod> Methods;
        public MenuMethod BackSpaceMethod;

        public ConsoleColor ItemColor { get; private set; }

        public ConsoleColor SelectionColor { get; private set; }

        public int SelectedItem { get; private set; }

        public Menu(MenuMethod backspaceMethod, params MenuMethod[] methods)
        {
            BackSpaceMethod = backspaceMethod;
            Methods = new List<MenuMethod>();
            Methods.AddRange(methods);
            ItemColor = ConsoleColor.White;
            SelectionColor = ConsoleColor.Yellow;
        }

        private int top;
        private int currentTop;

        public void Show()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Press Esc to exit");
            Console.WriteLine("Press Backspace to return back");
            Console.WriteLine("Warning, if it is first menu, program will closed");
            Console.ResetColor();
            currentTop = top = Console.CursorTop;
            for (int i = 0; i < Methods.Count; i++)
            {
                if (i == 0)
                {
                    Console.ForegroundColor = SelectionColor;
                }
                else {
                    Console.ResetColor();
                    Console.ForegroundColor = ItemColor;
                }
                Console.WriteLine(Methods[i].Method.Name);
            }
            Console.ResetColor();
            WaitForInput();
        }

        private void WaitForInput()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (SelectedItem != -1)
                            MoveDown();
                        break;
                    case ConsoleKey.UpArrow:
                        if (SelectedItem != -1)
                            MoveUp();
                        break;
                    case ConsoleKey.Enter:
                        Console.ResetColor();
                        if (SelectedItem >= 0 && SelectedItem < Methods.Count)
                        {
                            Methods[SelectedItem]();
                            SelectedItem = -1;
                        }
                        break;
                    case ConsoleKey.Backspace:
                        if (SelectedItem == -1)
                            SelectedItem = 0;
                        BackSpaceMethod();
                        return;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }

        private void MoveDown()
        {
            Console.ForegroundColor = ItemColor;
            Console.SetCursorPosition(0, currentTop);
            Console.WriteLine(Methods[SelectedItem].Method.Name);
            SelectedItem = SelectedItem == Methods.Count - 1 ? 0 : SelectedItem + 1;
            if (SelectedItem == 0)
                currentTop = top;
            else
                currentTop++;
            Console.ForegroundColor = SelectionColor;
            Console.SetCursorPosition(0, currentTop);
            Console.WriteLine(Methods[SelectedItem].Method.Name);
            Console.SetCursorPosition(0, currentTop);
        }

        private void MoveUp()
        {
            Console.ForegroundColor = ItemColor;
            Console.SetCursorPosition(0, currentTop);
            Console.WriteLine(Methods[SelectedItem].Method.Name);
            SelectedItem = SelectedItem == 0 ? Methods.Count - 1 : SelectedItem - 1;
            if (Methods.Count - 1 == SelectedItem)
                currentTop = top+ Methods.Count - 1;
            else
                currentTop--;
            Console.ForegroundColor = SelectionColor;
            Console.SetCursorPosition(0, currentTop);
            Console.WriteLine(Methods[SelectedItem].Method.Name);
            Console.SetCursorPosition(0, currentTop);

        }
    }
}
