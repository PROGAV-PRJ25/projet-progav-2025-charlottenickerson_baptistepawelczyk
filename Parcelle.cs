using System;

public class Parcelle
{
    // Position of the parcel on the grid
    public int Row { get; private set; }
    public int Col { get; private set; }
    
    // Color of the parcel
    public ConsoleColor Couleur { get; private set; }
    
    // Other characteristics that can be added later
    public string Type { get; set; } = "Normal";
    public int Fertilite { get; set; } = 100;
    public int Humidite { get; set; } = 50;
    
    // Constructor
    public Parcelle(int row, int col, ConsoleColor couleur)
    {
        Row = row;
        Col = col;
        Couleur = couleur;
    }
    
    // Method to display a detailed view of the parcel
    public void AfficherVueDetaillee(int posX, int posY, int largeur = 10, int hauteur = 10)
    {
        try
        {
            // Make sure the view will fit in the console window
            int maxX = Math.Min(posX + largeur, Console.WindowWidth - 1);
            int maxY = Math.Min(posY + hauteur + 7, Console.WindowHeight - 2); // +7 for the dashboard
            
            // Adjust dimensions if necessary
            largeur = Math.Min(largeur, maxX - posX);
            hauteur = Math.Min(hauteur, maxY - posY - 7);
            
            if (largeur < 3 || hauteur < 3)
            {
                // Too small to display meaningfully
                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                Console.WriteLine("Erreur: Pas assez d'espace pour afficher la parcelle.");
                return;
            }
            
            // Save current cursor position
            int originalX = Console.CursorLeft;
            int originalY = Console.CursorTop;
            
            Console.SetCursorPosition(posX, posY - 1);
            Console.WriteLine("PARCELLE SÉLECTIONNÉE:");
            
            // Display a larger view of the parcel
            for (int i = 0; i < hauteur; i++)
            {
                Console.SetCursorPosition(posX, posY + i);
                
                for (int j = 0; j < largeur; j++)
                {
                    if (i == 0 || i == hauteur - 1 || j == 0 || j == largeur - 1)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.BackgroundColor = Couleur;
                        Console.Write(" ");
                    }
                }
            }
            
            // Reset background color
            Console.BackgroundColor = ConsoleColor.Black;
            
            // Display compact parcel information directly under the parcel view
            AfficherTableauDeBordParcelle(posX, posY + hauteur, largeur);
            
            // Restore original cursor position
            Console.SetCursorPosition(originalX, originalY);
        }
        catch (Exception ex)
        {
            // In case of any error, display it at the bottom of the screen
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.WriteLine($"Erreur d'affichage: {ex.Message}");
        }
    }
    
    // Method to display the parcel's dashboard
    private void AfficherTableauDeBordParcelle(int posX, int posY, int largeur)
    {
        // Use a more compact display format
        Console.SetCursorPosition(posX, posY);
        Console.Write("┌" + new string('─', Math.Max(0, largeur - 2)) + "┐");
        
        Console.SetCursorPosition(posX, posY + 1);
        Console.Write($"│ Type: {Type.PadRight(Math.Max(0, largeur - 9))}│");
        
        Console.SetCursorPosition(posX, posY + 2);
        Console.Write($"│ Fert: {Fertilite}%{new string(' ', Math.Max(0, largeur - 10 - Fertilite.ToString().Length))}│");
        
        Console.SetCursorPosition(posX, posY + 3);
        Console.Write($"│ Hum: {Humidite}%{new string(' ', Math.Max(0, largeur - 9 - Humidite.ToString().Length))}│");
        
        Console.SetCursorPosition(posX, posY + 4);
        Console.Write("└" + new string('─', Math.Max(0, largeur - 2)) + "┘");
        
        // Show the real coordinates below
        Console.SetCursorPosition(posX, posY + 5);
        Console.Write($"Coords: [{Row},{Col}]");
    }
}