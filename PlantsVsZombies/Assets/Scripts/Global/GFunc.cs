using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static partial class GFunc 
{
    
    //유니티의 build settings에서 player settings로 가서 scripting define symbols에 DEBUG_MODE를 추가한다.
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object message)
    {
#if DEBUG_MODE
        Debug.Log(message);
#endif
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void LogWarning(object message)
    {
#if DEBUG_MODE
        Debug.LogWarning(message);
#endif
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Assert(bool condition)
    {
#if DEBUG_MODE
        Debug.Assert(condition);
#endif
    }

    //! GameObject 받아서 Text 컴포넌트 찾아서 text 필드 값 수정하는 함수
    public static void SetText(this GameObject target, string text)
    {
        Text textComponent = target.GetComponent<Text>();
        if(textComponent == null || textComponent == default) { return; }

        textComponent.text = text;
    }

    //! LoadScene 함수 래핑한다.
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //! 특정 오브젝트의 자식 오브젝트를 서치해서 컴포넌트를 찾아주는 함수
    public static T FindChildComponent<T>(
        this GameObject targetObj_, string objName_) where T : Component
    {
        T searchResultComponent = default(T);
        GameObject searchResultObj = default(GameObject);

        searchResultObj = targetObj_.FindChildObject(objName_);
        if(searchResultObj != null || searchResultObj != default)
        {
            searchResultComponent = searchResultObj.GetComponent<T>();
        }

        return searchResultComponent;
    }

    //! 특정 오브젝트의 자식 오브젝트를 서치해서 찾아주는 함수
    public static GameObject FindChildObject(
        this GameObject targetObj_, string objName_)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;

        for(int  i = 0; i < targetObj_.transform.childCount; i++)
        {
            searchTarget = targetObj_.transform.GetChild(i).gameObject;
            if(searchTarget.name.Equals(objName_))
            {
                searchResult = searchTarget;
                return searchResult;
            }   // if: 내가 찾고싶은 오브젝트 찾은 경우
            else
            {
                searchResult = FindChildObject(searchTarget, objName_);

                if(searchResult == null || searchResult == default) { /*Pass*/}
                else { return searchResult; }
            }   // else : 내가 찾고 싶은 오브젝트를 아직 못찾은 경우
        }   // loop: 탐색 타겟 오브젝트의 자식 오브젝트 갯수만큼 순회하는 루프

        return searchResult;
    }   // FindChildObject()

    //! 현재 씬의 이름을 리턴한다.
    public static string GetActiveSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    //! 활성화 된 현재 씬의 루트 오브젝트를 서치해서 찾아주는 함수
    public static GameObject GetRootObject(string objName_)
    {
        Scene activeScene_ = SceneManager.GetActiveScene();
        GameObject[] rootObjs_ = activeScene_.GetRootGameObjects();

        GameObject targetObj_ = default;
        foreach(GameObject rootObj_ in rootObjs_)
        {
            if(rootObj_.name.Equals(objName_))
            {
                targetObj_ = rootObj_;
                return targetObj_;
            }
            else
            {
                continue;
            }
        }
        return targetObj_;
    }

    //! 두 벡터를 더한다
    public static Vector2 AddVector(this Vector3 origin, Vector2 addVector)
    {
        Vector2 result = new Vector2(origin.x, origin.y);
        result += addVector;
        return result;
    }

    //! 컴포넌트가 존재하는지 여부를 체크하는 함수
    public static bool IsValid<T>(this T target) where T : Component
    {
        if(target == null || target == default)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //!  리스트가 유효한지 여부를 체크하는 함수

    public static bool IsValid<T>(this List<T> target) where T : Component
    {
        bool isInvalid = (target == null || target == default);
        isInvalid = isInvalid || target.Count == 0;
        if (isInvalid)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //public static void StartDelayedAction(float delaySeconds)
    //{
    //    StartCoroutine(DelayedAction(delaySeconds));
    //}


    //! 딜레이 거는 내용
    public static IEnumerator DelayedAction(float delaySeconds)
    {
        Debug.Log("Action started");

        yield return new WaitForSeconds(delaySeconds);

        Debug.Log("Action completed");
    }
}
