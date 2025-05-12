public class Persil:PlanteAromatique {

    public Persil(Terrain terrain) : base(terrain) {
        Nom ="Persil";
        Type = "Annuelle";
        Saisons = new List<string> {"Printemps", "Automne"};
        TerrainPrefere = "Marais";
        Espacement = 1;
        VitesseCroissance = 0.2;
        BesoinsEnEau = 2;
        BesoinsEnLuminosite = "Luminosité moyenne";
        TemperaturesPreferees = new List<int> {10, 22};
        MaladiesPossibles = new List<string> {"mildiou", "nématodes"};
        ProbabilitesMaladies = new List<int> {30, 25};
        EsperanceDeVie = 0.5;
        Rendement = 10;
        EtatPlante ="Germination";
    }
}