//
//  TDMFBSDKRequests.h
//  Tapdaq iOS SDK
//
//  Created by Mukund Agarwal on 05/09/2016.
//  Copyright © 2016 Tapdaq. All rights reserved.
//

#import <Foundation/Foundation.h>

#define FBSDK 1

#import "TDMFBViewController.h"

@class TDMFBViewController;

@protocol TDMFANDelegate;

@interface TDMFBSDKRequests : NSObject

@property (nonatomic, weak) id <TDMFANDelegate> delegate;
@property TDMFBViewController* FBViewController;

+ (instancetype)sharedInstance;

- (void)addTestDevices:(NSArray *)testDeviceIDs;

- (void)refreshAdNetwork;

- (bool)isBannerReady;

- (bool)isInterstitialReady:(NSString *)interstitialID;

- (void)loadInterstitialFBID:(NSString *)interstitialID;

- (void)showInterstitial;

- (void)loadBannerFB:(int)size
               forID:(NSString *)bannerID;

- (UIView *)getBanner;

@end

#pragma mark -
#pragma mark TDMFANDelegate

@protocol TDMFANDelegate <NSObject>

@optional

#pragma mark -
#pragma mark Banner

- (void)tapdaqFANWillDisplayBanner;

- (void)tapdaqFANDidDisplayBanner;

- (void)tapdaqFANDidFailToDisplayBanner;

- (void)tapdaqFANAuthErrorBanner;

- (void)tapdaqFANDidLoadBanner;

- (void)tapdaqFANDidClickBanner;

- (void)tapdaqFANDidFinishHandlingClickBanner;

#pragma mark -
#pragma mark Interstitial & Video

- (void)tapdaqFANDidLoadInterstitial;

- (void)tapdaqFANDidDisplayInterstitial;

- (void)tapdaqFANDidFailToDisplayInterstitial;

- (void)tapdaqFANAuthErrorInterstitial;

- (void)tapdaqFANDidCloseInterstitial;

- (void)tapdaqFANWillCloseInterstitial;

- (void)tapdaqFANDidClickInterstitial;

- (void)tapdaqFANWillDisplayInterstitial;

@end
