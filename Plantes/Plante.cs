public abstract class Plante {
    protected string Nom;
    protected string Type;
    protected bool MauvaisesHerbes;
    protected List<string> Saisons;
    protected Terrain Terrain;
    protected double Espacement;
    protected double VitesseCroissance;
    protected double BesoinsEnEau;
    protected double BesoinsEnLuminosite;
    protected List<int> TemperaturesPreferees;
    protected List<string> MaladiesPossibles;
    protected int EsperanceDeVie;
    protected int Rendement;
    protected EtatPlante EtatPlante; // Enum : Germination, Croissance, Mûre, Morte

    public Plante()
    {
        Type = "Annuelle";
        MauvaisesHerbes = false;
    }

    void Pousser(ConditionsEnvironnementales conditions);
    bool EstMorte();
}