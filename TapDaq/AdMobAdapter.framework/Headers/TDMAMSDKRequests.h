//
//  TDMAMSDKRequests.h
//  Tapdaq iOS SDK
//
//  Created by Mukund Agarwal on 09/09/2016.
//  Copyright © 2016 Tapdaq. All rights reserved.
//

#import <Foundation/Foundation.h>

#define AMSDK 1

#import "TDMAMViewController.h"

@class TDMAMViewController;
@class TDMAMSDKRequests;

@protocol TDMAMDelegate;

@interface TDMAMSDKRequests : NSObject

@property (nonatomic, weak) id <TDMAMDelegate> delegate;
@property TDMAMViewController* AMViewController;

+ (instancetype)sharedInstance;

- (void)addTestDevices:(NSArray *)testDeviceIDs;

- (void)refreshAdNetwork;

- (bool)isBannerReady;

- (bool)isInterstitialReady;

- (void)loadInterstitialAMID:(NSString *)interstitialID;

- (void)showInterstitial;

- (bool)isVideoReady;

- (void)loadVideoAMID:(NSString *)videoID;

- (void)showVideo;

- (void)loadBannerAM:(int)size
               forID:(NSString *)bannerID;

- (UIView *)getBanner;

@end

#pragma mark -
#pragma mark TDMAMDelegate

@protocol TDMAMDelegate <NSObject>

@optional

#pragma mark -
#pragma mark Banner

- (void)tapdaqAMDidLoadBanner;

- (void)tapdaqAMWillDisplayBanner;

- (void)tapdaqAMDidDisplayBanner;

- (void)tapdaqAMDidFailToDisplayBanner;

- (void)tapdaqAMAuthErrorBanner;

- (void)tapdaqAMDidClickBanner;

- (void)tapdaqAMDidFinishHandlingClickBanner;


#pragma mark -
#pragma mark Interstitial & Video

- (void)tapdaqAMDidLoadInterstitial;

- (void)tapdaqAMDidFailToDisplayInterstitial;

- (void)tapdaqAMAuthErrorInterstitial;

- (void)tapdaqAMDidCloseInterstitial;

- (void)tapdaqAMDidCloseVideoInterstitial;

- (void)tapdaqAMWillCloseInterstitial;

- (void)tapdaqAMDidDisplayInterstitial;

- (void)tapdaqAMDidClickInterstitial;

@end
