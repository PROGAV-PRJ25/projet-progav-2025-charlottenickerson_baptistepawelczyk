using System;
using System.Collections.Generic;

public static class PaysAsciiArt
{
    // Dictionnaire contenant les ASCII art pour chaque pays
    private static readonly Dictionary<string, string[]> AsciiArts = new Dictionary<string, string[]>
    {
        // ASCII art pour la France (paysage avec la Tour Eiffel et des vignes)
        {"France", new string[]
            {
                @"     _/\     /\_     ",
                @"    /   \___/   \    ",
                @"   /  |    |    |\   ",
                @"  /   |    |    | \  ",
                @" /    |____|____| /  ",
                @"/_____|_________|/   ",
                @"      |    |    |    ",
                @"      |____|____|    ",
                @"     _\\\\|/////_    ",
                @"    /   \___/   \    ",
                @"   /~~~|     |~~~\   ",
                @"  /    |_____|    \  ",
                @" /~~~~~\___/~~~~~\   ",
                @"/________________\   "
            }
        },
        
        // ASCII art pour l'Égypte (pyramides et palmier)
        {"Égypte", new string[]
            {
                @"          .          ",
                @"         /|\         ",
                @"        / | \        ",
                @"       /  |  \       ",
                @"      /   |   \      ",
                @"     /    |    \     ",
                @"    /_____^_____\    ",
                @"           |         ",
                @"          /|\        ",
                @"     __/\/___\/\__   ",
                @"    /___/:::::\___\  ",
                @"   /::::::|_|::::::\ ",
                @"   \:::::/ _ \:::::/  ",
                @"    \/\/\/ \/__/\/   "
            }
        },
        
        // ASCII art pour le Japon (mont Fuji et cerisier)
        {"Japon", new string[]
            {
                @"         .           ",
                @"        /|\          ",
                @"       / | \         ",
                @"      /__|__\        ",
                @"     /  /|\  \       ",
                @"    /__/_|_\__\      ",
                @"   /~~/ /|\ \~~\     ",
                @"  /~~/_/_|_\_\~~\    ",
                @" /~~~~~~~~~~~~~~\    ",
                @"   (  )   (  )       ",
                @"    \/     \/        ",
                @"     |     |         ",
                @"    /|\   /|\        ",
                @"   //|\\  //|\\      "
            }
        },
        
        // ASCII art pour Verdania (paysage luxuriant imaginaire)
        {"Verdania", new string[]
            {
                @"     _.,,,,,._       ",
                @"   .'  \  /  '.      ",
                @"  /   o \/ o   \     ",
                @" |    \★/    |     ",
                @" |   /\/\/\   |     ",
                @" |  / /  \ \  |     ",
                @"  \/ |    | \/      ",
                @"   \_\____/_/       ",
                @"  / /\    /\ \      ",
                @" //| \    / |\\     ",
                @"//||  \__/  ||\\    ",
                @"//||===::===||\\    ",
                @"//__\======/__\\    ",
                @"\\__/======\__//    "
            }
        },
        
        // ASCII art pour Aridium (désert avec cactus)
        {"Aridium", new string[]
            {
                @"      _     _        ",
                @"     | |   | |       ",
                @"     | |   | |       ",
                @"     | |___| |       ",
                @"   __|_|   |_|__     ",
                @"  /     \_/     \    ",
                @" /               \   ",
                @"/        .        \  ",
                @"        /|\          ",
                @"~~~~~~~~~~~~~~~~~~~~~~~",
                @"   .      .    .      ",
                @"  / \    / \  / \     ",
                @" /   \  /   \/   \    ",
                @"/     \/         \   "
            }
        }
    };
    
    // Méthode pour afficher l'ASCII art d'un pays avec une couleur spécifique
    public static void AfficherAsciiArt(string nomPays, ConsoleColor couleur)
    {
        if (AsciiArts.ContainsKey(nomPays))
        {
            Console.ForegroundColor = couleur;
            string[] lignes = AsciiArts[nomPays];
            
            foreach (string ligne in lignes)
            {
                Console.WriteLine(ligne);
            }
            
            Console.ResetColor();
        }
        else
        {
            // ASCII art par défaut pour les pays sans art spécifique
            string[] lignes = new string[]
            {
                @"    _____      ",
                @"   /     \     ",
                @"  /       \    ",
                @" |         |   ",
                @" |         |   ",
                @" |         |   ",
                @" |         |   ",
                @"  \       /    ",
                @"   \_____/     "
            };
            
            Console.ForegroundColor = couleur;
            foreach (string ligne in lignes)
            {
                Console.WriteLine(ligne);
            }
            Console.ResetColor();
        }
    }
}
