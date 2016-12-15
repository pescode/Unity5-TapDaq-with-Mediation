//
//  TDMCBSDKRequests.h
//  Tapdaq iOS SDK
//
//  Created by Mukund Agarwal on 15/09/2016.
//  Copyright © 2016 Tapdaq. All rights reserved.
//

#import <Foundation/Foundation.h>

#define CBSDK 1

#import "Chartboost/Chartboost.h"

//Debug flags
#define TDMCBDEBUG 1

#if defined(TDMCBDEBUG)
#define TDMCBLog(fmt, ...) NSLog((@"[%@] " fmt), [self class], ##__VA_ARGS__)
#else
#   define TDMCBLog(...)
#endif

#ifdef CBSDK

@protocol TDMCBDelegate;

@interface TDMCBSDKRequests : NSObject <ChartboostDelegate>
#else
@interface TDMCBSDKRequests : NSObject
#endif

@property (nonatomic, weak) id <TDMCBDelegate> delegate;

+ (instancetype)sharedInstance;

- (void)refreshAdNetwork;

- (void)configure:(NSString *)appID
     secondaryKey:(NSString *)appSignature;

- (bool)isInterstitialReady;

- (void)loadInterstitial;

- (void)showInterstitial;

- (bool)isVideoReady;

- (void)loadVideo;

- (void)showVideo;

- (bool)isRewardedVideoReady;

- (void)loadRewardedVideo;

- (void)showRewardedVideo;

@end

#pragma mark -
#pragma mark TDMCBDelegate

@protocol TDMCBDelegate <NSObject>

@optional

#pragma mark -
#pragma mark Interstitial

- (void)tapdaqCBWillDisplayInterstitial;

- (void)tapdaqCBDidDisplayInterstitial;

- (void)tapdaqCBDidLoadInterstitial;

- (void)tapdaqCBDidFailToDisplayInterstitial;

- (void)tapdaqCBWillCloseInterstitial;

- (void)tapdaqCBDidCloseInterstitial;

- (void)tapdaqCBDidClickInterstitial;

- (void)tapdaqCBAuthErrorInterstitial;

#pragma mark -
#pragma mark Video & Rewarded Video

- (void)tapdaqCBWillDisplayVideo;

- (void)tapdaqCBDidDisplayVideo;

- (void)tapdaqCBDidLoadVideo;

- (void)tapdaqCBDidFailToDisplayVideo;

- (void)tapdaqCBRemoveVideo;

- (void)tapdaqCBRemoveRewardedVideo;

- (void)tapdaqCBWillCloseVideo;

- (void)tapdaqCBDidCloseVideo;

- (void)tapdaqCBDidClickVideo;

- (void)tapdaqCBDidLoadRewardedVideo;

- (void)tapdaqCBLoadAnotherVideo;

- (void)tapdaqCBLoadAnotherRewardedVideo;

- (void)tapdaqCBRewardValidationSuceeded:(NSString *) rewardName
                            rewardAmount:(int) rewardAmount;

- (void)tapdaqCBAuthErrorVideo;

@end
