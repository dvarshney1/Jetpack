using UnityEngine;

public static class Utils{

    
    public static float smoothStart2(float factor){
        return factor * factor;

    }

    
    public static float smoothStop2(float factor){
        return 1-(1-factor)*(1-factor);

    }
    
    
    public static float smoothStop4(float factor){

        return 1-(1-factor)*(1-factor)*(1-factor)*(1-factor);

    }
    
    public static float smoothStop8(float factor){
        return 1-(1-factor)*(1-factor)*(1-factor)*(1-factor)*(1-factor)*(1-factor)*(1-factor)*(1-factor);

    }

}
