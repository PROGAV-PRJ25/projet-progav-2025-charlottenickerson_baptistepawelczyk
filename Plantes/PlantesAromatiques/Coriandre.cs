public class Coriandre:PlanteAromatique {

    public Coriandre(Terrain terrain) : base(terrain) {
        Nom ="Coriandre";
        Type = "Annuelle";
        Saisons = new List<string> {"Printemps", "Automne"};
        TerrainPrefere = "Forêt";
        Espacement = 1;
        VitesseCroissance = 0.133;
        BesoinsEnEau = 1.5;
        BesoinsEnLuminosite = "Luminosité moyenne";
        TemperaturesPreferees = new List<int> {10, 24};
        MaladiesPossibles = new List<string> {"fonte des semis"};
        ProbabilitesMaladies = new List<int> {30};
        EsperanceDeVie = 0.25;
        Rendement = 6;
        EtatPlante ="Germination";
    }
}