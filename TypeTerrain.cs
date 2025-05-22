// Classe utilitaire pour obtenir le nom du type de terrain
public static class TypeTerrain
{
    public static string GetNomTypeTerrain(Terrain terrain)
    {
        string typeComplet = terrain.GetType().ToString();
        
        // Extraire le nom de la classe sans l'espace de noms
        int dernierPoint = typeComplet.LastIndexOf('.');
        if (dernierPoint >= 0 && dernierPoint < typeComplet.Length - 1)
        {
            return typeComplet.Substring(dernierPoint + 1);
        }
        
        return typeComplet;
    }
}
