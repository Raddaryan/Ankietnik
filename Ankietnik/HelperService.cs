using System.Web.UI;

namespace Ankietnik
{
    /// <summary>
    /// Klasa pomocnicza zawierające generyczne metody nie związane z logiką 'biznesową' aplikacji (utils).
    /// </summary>
    public static class HelperService
    {
        /// <summary>
        /// Metoda zwracająca kontrolkę o podanej wartości ID w całym drzeiwe potomków kontrolki 'root'.
        /// </summary>
        /// <param name="root">Dla wyszukiwania w całej stronie należy przekazać kontrolkę 'Page'</param>
        internal static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
            {
                return root;
            }

            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, id);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }
    }
}