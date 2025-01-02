// using UnityEngine;
//
// public class FloatValueMultiplier : ValueProcessor<float>
// {
//     [SerializeField] private float multiplier = 1;
//     
//     public override float ProcessValue(float inputValue)
//     {
//         return inputValue * multiplier;
//     }
//
//     public void SetMultiplier(float multiplierValue)
//     {
//         multiplier = multiplierValue;
//     }
//     
//     public void SetMultiplier(FloatContainer multiplierValueContainer)
//     {
//         SetMultiplier(multiplierValueContainer.GetValue);
//     }
// }