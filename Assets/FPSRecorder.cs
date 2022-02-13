using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//需要把这个脚本拖放到一个UI物体上哦
[RequireComponent(typeof(Text))]//需要有Text组件
public class FPSRecorder : MonoBehaviour
{

    const float fpsMeasurePeriod = 0.5f;//固定时间为0.5s
    private int m_FpsAccumulator = 0;
    private float m_FpsNextPeriod = 0;
    private int m_CurrentFps;
    const string display = "{0} FPS";//输出格式

    private Text mText;

    private void Start()
    {
        //Time.realtimeSinceStartup  游戏开始的真实时间
        m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;//下一次刷新时间
        mText = this.GetComponent<Text>();
    }
    private void Update()
    {
        // measure average frames per second
        //计算每秒的平均帧数
        m_FpsAccumulator++;
        if (Time.realtimeSinceStartup > m_FpsNextPeriod)
        {
            m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
            m_FpsAccumulator = 0;
            m_FpsNextPeriod += fpsMeasurePeriod;
            mText.text = string.Format(display, m_CurrentFps);
        }
    }

}