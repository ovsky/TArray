# 📦 ReTween - Fastest Unity and .NET Tweening Solution 

![ReTween Logo](https://i.postimg.cc/150Fwqkd/ellipse1ss275.png)

ReTween is blazing fast, modular and really simple Tweening System for Unity.

This plugin is not ispired by market-leading DoTween, beacuse we think that the advanced and fast tweening can be done way better. 

Beacuse of that, the structure of writing Tweens is quite different than in DoTween - way more clear and elegant. But we do not rule out that in the future we will run an overlay that allows you to use ReTween, like DoTween - for people which are used to using DoTween.

[Benchmarks comparing ReTween and DoTween will be available shortly]

## 📖 QuickStart

### ReTween:

So, let's tween a color and piosition of objects in two different ways:

```csharp
Tween.Color(someImage, Color.blue, 1f, 0.5f, Ease.InOut);
Tween.Position(someTransform, Vector3.right);
```

Pretty easy, yeah?

### 🗂️ ReExtensions:
ReTween supports also extensions like: 
`ReTween.Next()` in which you define the next action, after reffered. 
`ReTween.SetEase()` where you can set easing mode, after creating a `Tween`.
And more...

### 📚 ReModules:

You can create new ReModule by writing new script in ReTween/Modules that will use the Tween.Add() method, and create new `Action<float>`, where the `float` is easing time, from 0 to 1.

If you want to use it like the rest of the ReTween *(by Tween.YourModule())*, you can make your new class a *partial* of the Tween.

For example, we will add a Color Lerp module for UI.Image:

```csharp
public static void Color(Image image, Color target)
{
    Tween.Add(new TweenAction((float time) => image.color = UnityEngine.Color.LerpUnclamped(image.color, target, time)));
}
```

Obviously, you can create more complex Tweens. In standard TweenAction you can use more pre-implemented values, like: `duration`, `delay`, `easing`, and more.

```csharp
public static void Color(Image image, Color target, float duration = 1f, float delay = 0f, Ease ease = null)
{
    Tween.Add(new TweenAction((float time) => image.color = UnityEngine.Color.LerpUnclamped(image.color, target, time), duration, delay, ease));
}
```


### 📘 Ease / Easing:

#### How easing works?

Easings are some sort of simple curves, that helps to visualise and evaluate smooth and juicy, animations. You can use some of the predefined curves or create own.

In simple words - it evaluates the 0.0-1.0 value that you privided, creating the new one, based on your Ease, abling you to easile make really nice visuals. 

Easing Visualisation:
![](https://i.ibb.co/tX2dMRV/1-0-Z40-Vvur-Cgo-GJb-Kjj-In-Dl-Ax.gif)
##### @leonardodev

#### Technical Background

*Ease* is fully compatible and swapable with AnimationCurves - with which you may already be familiar - with no additional instructions. 
In every field you have used AnimationCurve, you can use Ease as the replacement, without changing the implementation. 
The most important difference is that Eases has many predefined, basic values, a bank of custom definition, and that they are completely garbage-free, and made with optimisation in-mind. 

### ⏩ Supported high-performace Ease Calculations:

**ReTween** provides implementation of 28 high-performance math patterns for easing types:

```csharp

  
    Linear = 0,
    QuadIn = 1,
    QuadOut = 2,
    QuadInOut = 3,
    CubicIn = 4,
    CubicOut = 5,
    CubicInOut = 6,
    QuartIn = 7,
    QuartOut = 8,
    QuartInOut = 9,
    QuintIn = 10,
    QuintOut = 11,
    QuintInOut = 12,
    BounceIn = 13,
    BounceOut = 14,
    BounceInOut = 15,
    ElasticIn = 16,
    ElasticOut = 17,
    ElasticInOut = 18,
    CircularIn = 19,
    CircularOut = 20,
    CircularInOut = 21,
    SinusIn = 22,
    SinusOut = 23,
    SinusInOut = 24,
    ExponentialIn = 25,
    ExponentialOut = 26,
    ExponentialInOut = 27
  
```


## 📗 API Reference

#### The structure of basic *TweenAction*:

| TweenAction | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `start` | `float` | TweenAction start time. *[auto assigned][read-only]* |
| `action` | `Action<float>` | Main action, that receives the float value as lerp time. |
| `delay` | `float` | Time to start invoking TweenAction |
| `duration` | `float` | Duration of tweening process. |
| `ease` | `Ease` | Selected Ease, to make tweening more pretty.  |


## 📝 License:

Copyright (c) 2023 - Przemysław Orłowski

License: 

"THE SOFTWARE IS PROVIDED 'AS IS' (...)" also known as:

**MIT License**: https://choosealicense.com/licenses/mit/

---

Have fun!