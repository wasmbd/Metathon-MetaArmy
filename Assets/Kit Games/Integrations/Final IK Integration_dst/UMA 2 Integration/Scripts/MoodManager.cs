using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
using UMA.PoseTools;
public class MoodManager : MonoBehaviour
{
    public enum Moods { Neutral, Happy, Sad }
    public Moods myMood;
    
    private DynamicCharacterAvatar avatar;
    private ExpressionPlayer expression;
    private bool connected;


    private Moods lastMood = Moods.Neutral;

    private void OnEnable()
    {
        avatar = GetComponent<DynamicCharacterAvatar>();
        avatar.CharacterCreated.AddListener(OnCharacterCreated);
        avatar.RecipeUpdated.AddListener(OnCharacterRecipeUpdated);


    }

    private void OnDisable()
    {
        avatar.CharacterCreated.RemoveListener(OnCharacterCreated);

    }

    void OnCharacterCreated(UMAData data)
    {
        ChangeMood(myMood);
    }

    void OnCharacterRecipeUpdated(UMAData data)
    {
        ChangeMood(myMood);
        

    }

    public void ChangeMood(Moods mood)
    {
        expression = GetComponent<ExpressionPlayer>();
        expression.enableBlinking = true;
        expression.enableSaccades = true;
        expression.overrideMecanimJaw = true;
        connected = true;
        if (connected)
        {
            switch (mood)
            {
                case Moods.Neutral:
                    expression.leftMouthSmile_Frown = 0f;
                    expression.rightMouthSmile_Frown = 0f;
                    expression.leftEyeOpen_Close = 0f;
                    expression.rightEyeOpen_Close = 0f;
                    expression.midBrowUp_Down = 0f;

                    break;

                case Moods.Happy:
                    expression.leftMouthSmile_Frown = 0.7f;
                    expression.rightMouthSmile_Frown = 0.7f;
                    expression.leftEyeOpen_Close = -0.3f;
                    expression.rightEyeOpen_Close = -0.3f;
                    expression.midBrowUp_Down = 0.4f;

                    break;

                case Moods.Sad:

                    expression.leftMouthSmile_Frown = -0.7f;
                    expression.rightMouthSmile_Frown = -0.7f;
                    expression.leftEyeOpen_Close = 0.3f;
                    expression.rightEyeOpen_Close = 0.3f;
                    expression.midBrowUp_Down = -0.4f;

                    break;
                default:
                    break;
            }
        }
       
    }


}
