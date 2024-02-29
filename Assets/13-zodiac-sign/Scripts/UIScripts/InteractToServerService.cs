using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace _13_zodiac_sign.Scripts.UIScripts
{
    public class InteractToServerService : MonoBehaviour
    {
        /// <summary>
        /// 이메일 패스워드 입력받아 JWT 인증 토큰을 받아오는 동작을 수행합니다.
        /// </summary>
        /// <param name="email"> 유저 이메일 </param>
        /// <param name="password"> 유저 패스워드 </param>
        /// <returns></returns>
        public static IEnumerator Login(String email, String password) {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get("http://localhost:8080/user");
            
            var emailAndPassword = email+":"+password;
            string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(emailAndPassword));
            
            unityWebRequest.SetRequestHeader("Authorization", "Basic "+ base64String);
            unityWebRequest.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
            
            yield return unityWebRequest.SendWebRequest();
 
            if (unityWebRequest.result != UnityWebRequest.Result.Success) {
                // Http 통신 실패에 대한 예외 처리 필요
            }
            else {
                Debug.Log(unityWebRequest.downloadHandler.text);
                
                // Or retrieve results as binary data
                String responseHeaderKeyValueString = "";
                foreach (var responseHeader in unityWebRequest.GetResponseHeaders())
                {
                    responseHeaderKeyValueString += responseHeader.Key + ':' + responseHeader.Value + "\n";
                }
                Debug.Log(responseHeaderKeyValueString);
                
                UserInfoManager.Inst.UserJwtToken = unityWebRequest.GetResponseHeader("Authorization");
                UserInfoManager.Inst.UserEmail = email;
            }
            yield return null;
        }
    }
}