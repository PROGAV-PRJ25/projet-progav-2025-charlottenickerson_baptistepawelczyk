public abstract class PlanteCommerciale:Plante {

    public PlanteCommerciale(Terrain terrain) : base(terrain) {
        EstComestible = false;
    }
}