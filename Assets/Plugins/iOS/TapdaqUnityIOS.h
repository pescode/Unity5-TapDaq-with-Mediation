//
//  TapdaqUnityIOS.h
//  TapdaqUnity
//
//  Created by Rheo Violenes on 05/05/15.
//  Copyright (c) 2015 Nerd. All rights reserved.
//
#import <Foundation/Foundation.h>
#import <Tapdaq/Tapdaq.h>

// This is the bridge to Unity.
// Unity can only see and call methods outside the @interface
typedef void (*InterstitialCallback)();
typedef void (*NativeCallback)();

typedef void (*InterstitialDelegateCallback)();
typedef void (*NativeDelegateCallback)();


void _GenerateCallBacks(NativeDelegateCallback nativeCallback,
                        InterstitialDelegateCallback interstitialCallback);

void _ConfigureTapdaq(const char* appIdChar,
                      const char* clientKeyChar,
                      int frequencyCap,
                      int frequencyCapDurationInDays,
                      const char* enabledAdTypesChar,
                      bool testMode);

void _FetchNative(NativeCallback callback,
                  int adTypeInt);
void _FetchNativeAdWithTag (NativeCallback callback,
                            NSString* tag,
                            int nativeAdType);

void _SendNativeClick(int index);
void _SendNativeImpression(int index);

void _LaunchMediationDebugger();
void _AddTestDevices(const char* adNetwork, const char* deviceIDs);

// interstitial
void _ShowInterstitial(int* _orientation);
void _ShowInterstitialWithTag(const char* tagChar);

// banner
void _RequestBanner (int size);
void _RequestBannerWithTag (const char* tagChar, int size);
void _ShowBanner ();
void _ShowBannerWithTag (const char* tagChar);

// video
void _ShowVideo ();
void _ShowVideoWithTag (const char* tagChar);

// reward video
void _ShowRewardVideo ();
void _ShowRewardVideoWithTag (const char* tagChar);

@interface TapdaqUnityIOS : NSObject <TapdaqDelegate>

@property (nonatomic) int interstitialPointer;
@property (nonatomic) int nativePointer;

@property (nonatomic, strong, retain) NSMutableArray *nativeAdList;
@property (nonatomic, strong, retain) NSMutableArray *interstitialAdList;

@property (nonatomic) NativeDelegateCallback NativeDelegateCallBackObj;
@property (nonatomic) InterstitialDelegateCallback InterstitialDelegateCallbackObj;

+ (instancetype)sharedInstance;

- (void)retainer;

- (void)associateCallbacksToNativeDelegate:(NativeDelegateCallback)nativeCallback
                      interstitialDelegate:(InterstitialDelegateCallback)interstitialCallback;

- (void)initWithApplicationId:(NSString *)appID
                    clientKey:(NSString *)clientKey
                 frequencyCap:(int)frequencyCap
   frequencyCapDurationInDays:(int)durationInDays
               enabledAdTypes:(NSString *)enabledAdTypes
                     testMode:(BOOL)testMode;

- (void)fetchNativeForAdType:(TDNativeAdType)adType
                    callback:(NativeCallback)callback;

- (void)fetchNativeForWithTag:(NSString *)tag
                       AdType:(TDNativeAdType)adType
                     callback:(NativeCallback)callback;

- (void)triggerClickForNativeAdvertAtIndex:(int)index;
- (void)triggerImpressionForNativeAdvertAtIndex:(int)index;

- (void)launchMediationDebugger;

- (void)showInterstitial;

- (void)requestBanner:(TDMBannerSize)size;
- (void)showBanner;

- (void)showVideo;

- (void)showRewardVideo;

@end
