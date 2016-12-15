//
//  Tapdaq.h
//  Tapdaq
//
//  Created by Tapdaq <support@tapdaq.com>
//  Copyright (c) 2016 Tapdaq. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

#import "TDOrientationEnum.h"
#import "TDNativeAdUnitEnum.h"
#import "TDNativeAdSizeEnum.h"

#import "TDAdTypeEnum.h"
#import "TDNativeAdTypeEnum.h"
#import "TDMNetworkEnum.h"
#import "TDMBannerSizeEnum.h"

@protocol TapdaqDelegate;

@class TDAdvert;
@class TDNativeAdvert;
@class TDInterstitialAdvert;
@class TDProperties;
@class TDPlacement;

typedef NSString *const TDPTag;

// Default.
static TDPTag const TDPTagDefault = @"default";
// Bootup - Initial bootup of game.
static TDPTag const TDPTagBootup = @"bootup";
// Home Screen - Home screen the player first sees.
static TDPTag const TDPTagHomeScreen = @"home_screen";
// Main Menu - Menu that provides game options.
static TDPTag const TDPTagMainMenu = @"main_menu";
// Pause - Pause screen.
static TDPTag const TDPTagPause = @"pause";
// Level Start - Start of the level.
static TDPTag const TDPTagLevelStart = @"start";
// Level Complete - Completion of the level.
static TDPTag const TDPTagLevelComplete = @"level_complete";
// Game Center - After a user visits the Game Center.
static TDPTag const TDPTagGameCenter = @"game_center";
// IAP Store - The store where the player pays real money for currency or items.
static TDPTag const TDPTagIAPStore = @"iap_store";
// Item Store - The store where a player buys virtual goods.
static TDPTag const TDPTagItemStore = @"item_store";
// Game Over - The game over screen after a player is finished playing.
static TDPTag const TDPTagGameOver = @"game_over";
// Leaderboard - List of leaders in the game.
static TDPTag const TDPTagLeaderBoard = @"leaderboard";
// Settings - Screen where player can change settings such as sound.
static TDPTag const TDPTagSettings = @"settings";
// Quit - Screen displayed right before the player exits a game.
static TDPTag const TDPTagQuit = @"quit";

@interface Tapdaq : NSObject

@property (nonatomic, weak) id <TapdaqDelegate> delegate;

/**
 The singleton Tapdaq object, use this for all method calls
 
 @return The Tapdaq singleton.
 */
+ (instancetype)sharedSession;


#pragma mark Initializing Tapdaq

/**
 A setter for the Application ID of your app, and the Client Key associated with your Tapdaq account. You can obtain these details when you sign up and add your app to https://tapdaq.com
 You must use this in the application:didFinishLaunchingWithOptions method. By default, test adverts is not enabled.
 Only intersitials will be fetched, to enable native adverts, use -setApplicationId:clientKey:properties:.
 
 @param applicationId The application ID tied to your app.
 @param clientKey The client key tied to your app.
 */
- (void)setApplicationId:(NSString *)applicationId
               clientKey:(NSString *)clientKey;

/**
 This overloaded method takes in an additional testMode boolean, where you can toggle test adverts.
 Only intersitials will be fetched, to enable native adverts, use -setApplicationId:clientKey:properties:.
 
 @param applicationId The application ID tied to your app.
 @param clientKey The client key tied to your app.
 @param testMode Set to YES if test adverts should be displayed, otherwise NO will display live ads.
 */
- (void)setApplicationId:(NSString *)applicationId
               clientKey:(NSString *)clientKey
                testMode:(BOOL)testMode;

/**
 This overloaded method takes in a TDProperties object, use this method to change the default configuration.
 
 @param applicationId The application ID tied to your app.
 @param clientKey The client key tied to your app.
 @param properties The properties object that overrides the Tapdaq defaults. See TDProperties for info on all configuration options.
 */
- (void)setApplicationId:(NSString *)applicationId
               clientKey:(NSString *)clientKey
              properties:(TDProperties *)properties;


#pragma mark Interstitial

/**
 Loads an interstitial.
 */
- (void)loadInterstitial;

/**
 Loads an interstitial for a specific placement tag.
 
 @param tag The placement tag.
 */
- (void)loadInterstitialForPlacementTag:(NSString *)tag;

/** 
 Displays an interstitial to the user, if an interstitial is available to be shown. 
 */
- (void)showInterstitial;

/**
 This method gives you greater control over where the interstitial appears in the view heirarchy.
 
 @param view The view which the interstitial view is added to as a subview.
 */
- (void)showInterstitial:(UIView *)view;

/**
 Displays an interstitial filtered by a given placement tag.
 
 @param tag The placement tag.
 **/
- (void)showInterstitialForPlacementTag:(NSString *)tag;

/**
 This method also filters interstitials by placement tag, as well as giving you greater control over where the 
 interstitial appears in the view heirarchy.
 You must register the tag in TDProperties otherwise adverts will not display.
 
 @param view The view which the interstitial view is added to as a subview.
 @param tag The placement tag.
 */
- (void)showInterstitial:(UIView *)view forPlacementTag:(NSString *)tag;

/**
 Displays a cross promo interstitial filtered by a given placement tag.
 
 @param view The view which the interstitial view is added to as a subview.
 @param tag The placement tag.
 **/
- (void)showCrossPromoInterstitial:(UIView *)view forPlacementTag:(NSString *)tag;


#pragma mark Video

/**
 Loads a video.
 */
- (void)loadVideo;

/**
 Loads a video for a specific placement tag.

 @param tag The placement tag.
 */
- (void)loadVideoForPlacementTag:(NSString *)tag;

/**
 Displays a video to the user, if a video is available to be shown.
 */
- (void)showVideo;

/**
 Displays a video filtered by a given placement tag.
 
 @param tag The placement tag.
 **/
- (void)showVideoForPlacementTag:(NSString *)tag;


#pragma mark Rewarded Video

/**
 Loads a rewarded video.
 */
- (void)loadRewardedVideo;

/**
 Loads a rewarded video for a specific placement tag.

 @param tag The placement tag.
*/
- (void)loadRewardedVideoForPlacementTag:(NSString *)tag;

/**
 Displays a rewarded video to the user, if a RewardedVideo is available to be shown.
 */
- (void)showRewardedVideo;

/**
 Displays a rewarded video filtered by a given placement tag.
 
 @param tag The placement tag.
 **/
- (void)showRewardedVideoForPlacementTag:(NSString *)tag;


#pragma mark Banner

/**
 Loads a banner for the specified size.
 
 @param size The banner size.
 */
- (void)loadBanner:(TDMBannerSize)size;

/**
 Loads a banner for a specific placement tag and size.
 
 @param tag The placement tag.
 @param size The banner size.
 */
- (void)loadBannerForPlacementTag:(NSString *)tag
                             size:(TDMBannerSize)size;

/**
 Displays a banner to the user, if a banner is available to be shown.
 */
- (UIView *)showBanner;

/**
 Displays a banner filtered by a given placement tag.
 
 @param tag The placement tag.
 **/
- (UIView *)showBannerForPlacementTag:(NSString *)tag;


#pragma mark Native adverts

/**
 Fetches a TDNativeAdvert which, unlike -showInterstitial, gives you full control over the UI layout. 
 This advert will include the already-fetched creative, icon, and other data such as app name, description, etc.
 You must implement -triggerImpression: and -triggerClick: when using this method.
 
 @param nativeAdType The native advert type to be fetched.
 @return A TDNativeAdvert.
 */
- (TDNativeAdvert *)getNativeAdvertForAdType:(TDNativeAdType)nativeAdType;

/**
 Fetches a TDNativeAdvert for a particular placement tag.
 You must register the tag in TDProperties otherwise adverts will not display.
 
 @param tag The placement tag
 @param nativeAdType The native advert type to be fetched.
 */
- (TDNativeAdvert *)getNativeAdvertForPlacementTag:(NSString *)tag adType:(TDNativeAdType)nativeAdType;

/**
 This method must be called when the advert is displayed to the user. You do not need to call this method when using -showInterstitial. 
 This should only be used when either a TDInterstitialAdvert or TDNativeAdvert has been fetched.
 
 @param advert The TDAdvert that has been displayed to the user, this can be a TDInterstitialAdvert or TDNativeAdvert.
 */
- (void)triggerImpression:(TDAdvert *)advert;

/**
 This method must be called when a user taps on the advert, you do not need to call this method when using -showInterstitial. 
 This should only be used when either TDInterstitialAdvert or TDNativeAdvert has been fetched.
 Unlike -triggerImpression:, this method will also direct users to the the App Store, or to a custom URL, depending on the adverts configuration.
 
 @param advert The TDAdvert that has been displayed to the user, this can be a TDInterstitialAdvert or TDNativeAdvert.
 */
- (void)triggerClick:(TDAdvert *)advert;

#pragma mark Mediation* mode

/**
 Used only when mediation mode is enabled, see TDProperties. 
 Loads a native advert, this will fetch the native advert's creative, and call either -didLoadNativeAdvert:forAdType: if the advert is successfully loaded, or -didFailToLoadNativeAdvertForAdType: if it fails to load. 
 We recommend you implement both delegate methods to handle the advert accordingly.
 
 @param nativeAdType The native advert type to be loaded.
 */
- (void)loadNativeAdvertForAdType:(TDNativeAdType)nativeAdType;

/**
 Used only when mediation mode is enabled, see TDProperties.
 Loads a native advert for a particular placement tag, this will fetch the native advert's creative, and call either -didLoadNativeAdvert:forPlacementTag:adType: if the advert is successfully loaded, or -didFailToLoadNativeAdvertForPlacementTag:adType: if it fails to load.
 We recommend you implement both delegate methods to handle the advert accordingly.
 
 @param tag The placement tag of the advert to be loaded.
 @param nativeAdType The native ad type of the advert to be loaded.
 */
- (void)loadNativeAdvertForPlacementTag:(NSString *)tag adType:(TDNativeAdType)nativeAdType;

/**
 Used only when mediation mode is enabled, see TDProperties.
 Loads an interstitial advert, this will fetch the interstitial's creative, and calls either -didLoadInterstitial:forOrientation: if the advert is successfully loaded, or -didFailToLoadInterstitialForOrientation: if it fails to load.
 We recommend you implement both delegate methods to handle the advert accordingly.
 
 @param orientation The orientation of the interstitial to be loaded.
 */
- (void)loadInterstitialAdvertForOrientation:(TDOrientation)orientation;

/**
 Used only when mediation mode is enabled, see TDProperties.
 Loads an interstitial advert for a particular placement tag, this will fetch the interstitial's creative, and calls either -didLoadInterstitial:forPlacementTag:orientation: if the advert is successfully loaded, or -didFailToLoadInterstitialForPlacementTag:orientation: if it fails to load.
 We recommend you implement both delegate methods to handle the advert accordingly.
 */
- (void)loadInterstitialAdvertForPlacementTag:(NSString *)tag orientation:(TDOrientation)orientation;

#pragma mark Misc

/**
 This method is only used for plugins such as Unity which do not automatically trigger the launch request on application bootup.
 */
- (void)launch;

/******************
 Deprecated methods
 ******************/

- (void)setApplicationId:(NSString *)applicationId
               clientKey:(NSString *)clientKey
                  config:(NSDictionary *)config __deprecated_msg("Use setApplicationId:clientKey:properties:");

- (void)setApplicationId:(NSString *)applicationId
               clientKey:(NSString *)clientKey
             orientation:(TDOrientation)orientation __deprecated_msg("Use setApplicationId:clientKey:properties:");

- (TDNativeAdvert *)getNativeAdvertForAdUnit:(TDNativeAdUnit)adUnit
                                        size:(TDNativeAdSize)adSize __deprecated_msg("Use getNativeAdvertForType:");

- (TDNativeAdvert *)getNativeAdvertForAdUnit:(TDNativeAdUnit)adUnit
                                        size:(TDNativeAdSize)adSize
                                 orientation:(TDOrientation)orientation __deprecated_msg("Use getNativeAdvertForType:");

- (void)loadNativeAdvertForAdUnit:(TDNativeAdUnit)adUnit
                             size:(TDNativeAdSize)adSize
                      orientation:(TDOrientation)orientation __deprecated_msg("Use loadNativeAdvertForAdType:");

- (void)sendImpression:(TDAdvert *)advert __deprecated_msg("Use triggerImpression:");

- (void)sendClick:(TDAdvert *)advert __deprecated_msg("Use triggerClick:");

#pragma mark -
#pragma mark Mediation debug view

/**
 Used to launch the debugger view to test if ads can be shown
 
 @param vc The view controller which will display the launch debugger view.
 
 */
- (void)launchMediationDebugger:(UIViewController *)vc;

#pragma mark Initializing Tapdaq Mediation Ad Networks

/**
 Sets the test devices
 */

- (void)addTestDevices:(TDMNetwork)adNetwork
         testDeviceIDs:(NSArray *)testDeviceIDs;

/**
 Refreshes the ad network - useful for debug view controller only
 */

- (void)refreshAdNetwork:(TDMNetwork)adNetwork;

#pragma mark Direct mediation (debugger functions)

/**
 Loads an interstitial from one of the available ad networks
 */
- (void)loadDirectInterstitial:(TDMNetwork)adNetwork;

/**
 Displays an interstitial to the user from one of the available ad networks
 */
- (void)showDirectInterstitial:(TDMNetwork)adNetwork;

/**
 Loads a video from one of the available ad networks
 */
- (void)loadDirectVideo:(TDMNetwork)adNetwork;

/**
 Displays a video to the user from one of the available ad networks
 */
- (void)showDirectVideo:(TDMNetwork)adNetwork;

/**
 Loads a rewarded video from one of the available ad networks
 */
- (void)loadDirectRewardedVideo:(TDMNetwork)adNetwork;

/**
 Displays a rewarded video to the user from one of the available ad networks
 */
- (void)showDirectRewardedVideo:(TDMNetwork)adNetwork;

/**
 Loads a banner from one of the available ad networks
 */
- (void)loadDirectBanner:(TDMNetwork)adNetwork
                      size:(TDMBannerSize)size;

/**
 Returns a banner to the user from one of the available ad networks
 */
- (UIView *)showDirectBanner:(TDMNetwork)adNetwork;

#pragma mark Mediated Ad extra functions

/**
 Checks if an ad network has been setup already.
 */
- (BOOL)isAdNetworkSetup:(TDMNetwork)adNetwork;

/**
 This method is only used by the debugger to query if the launch request completed.
 */
- (bool)launchRequestCompleted;

/**
 This method is only used by the debugger to query if the app has a specific network enabled.
 */
- (bool)usingNetwork:(TDMNetwork)network;

/**
 This method is only used by the debugger to query if the app is using adapter or SDK for a specific network.
 */
- (bool)usingOfficialAdapter:(TDMNetwork)network;

@end



#pragma mark -
#pragma mark TapdaqDelegate

@protocol TapdaqDelegate <NSObject>

@optional

#pragma mark Common delegate methods

/**
 Called immediately after the Tapdaq is initialied.
 */
- (void)didLoadTapdaq;

#pragma mark Banner delegate methods

/**
 Called immediately after the banner is loaded.
 This method is only used in conjunction with -showMediatedBanner.
 */
- (void)didLoadBanner;

/**
 Called immediately before the banner is to be displayed to the user.
 This method is only used in conjunction with -showMediatedBanner.
 */
- (void)willDisplayBanner;

/**
 Called immediately after the banner is displayed to the user.
 This method is only used in conjunction with -showMediatedBanner.
 */
- (void)didDisplayBanner;

/**
 Called when, for whatever reason, the banner was not able to be displayed.
 This method is only used in conjunction with -showMediatedBanner.
 */
- (void)didFailToDisplayBanner;

/**
 Called when, for whatever reason, the banner was not able to be displayed for a specific network.
 This method is only used in conjunction with -showMediatedBanner.
 */
- (void)didFailToDisplayBannerForNetwork:(TDMNetwork)network;

/**
 Called when the user clicks the banner.
 This method is only used in conjunction with -showMediatedBanner.
 */
- (void)didClickBanner;

/**
 Called when the user clicked banner ad hasfinished and the user is returned to the app.
 This method is only used in conjunction with -showMediatedBanner.
 */
- (void)didFinishHandlingClickBanner;

#pragma mark Interstitial delegate methods

/**
 Called immediately after an interstitial is available to the user.
 This method is only used in conjunction with -showInterstitial.
 */
- (void)didLoadInterstitial;

/**
 Called immediately after an interstitial is available to the user for a specific placement tag.
 This method should be used in conjunction with -showInterstitialForPlacementTag:.
 @param tag A placement tag.
 */
- (void)didLoadInterstitialForPlacementTag:(NSString *)tag;

/**
 Called each time an interstitial is ready to be displayed.
 By default this method may be called multiple times on application launch, for each supported orientation.
 
 @param orientation The orientation of the interstitial that is ready to be displayed.
 */
- (void)didLoadInterstitialForOrientation:(TDOrientation)orientation;

/**
 Called each time an interstitial is ready to be displayed for a particular placement tag.
 @param tag The placement tag of the interstitial that is ready to be displayed.
 @param orientation The orientation of the interstitial that is ready to be displayed.
 */
- (void)didLoadInterstitialForPlacementTag:(NSString *)tag orientation:(TDOrientation)orientation;

/**
 Called immediately before the interstitial is to be displayed to the user.
 This method is only used in conjunction with -showInterstitial.
 */
- (void)willDisplayInterstitial;

/**
 Called immediately after the interstitial is displayed to the user.
 This method is only used in conjunction with -showInterstitial.
 */
- (void)didDisplayInterstitial;

/**
 Called when, for whatever reason, the interstitial was not able to be displayed.
 This method is only used in conjunction with -showInterstitial.
 */
- (void)didFailToDisplayInterstitial;

/**
 Called when, for whatever reason, the interstitial was not able to be displayed for a specific network.
 This method is only used in conjunction with -showInterstitial.
 */
- (void)didFailToDisplayInterstitialForNetwork:(TDMNetwork)network;

/**
 Called when the interstitial was not able to be displayed for a specific placement tag.
 This method should be used in conjunction with -showInterstitialForPlacementTag:.
 @param tag A placement tag.
 */
- (void)didFailToShowInterstitialForPlacementTag:(NSString *)tag;

/**
 Called when the user closes interstitial, either by tapping the close button, or the background surrounding the interstitial.
 This method is only used in conjunction with -showMediatedInterstitial.
 */
- (void)willCloseInterstitial;

/**
 Called when the user closes interstitial, either by tapping the close button, or the background surrounding the interstitial.
 This method is only used in conjunction with -showInterstitial.
 */
- (void)didCloseInterstitial;

/**
 Called when the user clicks the interstitial.
 This method is only used in conjunction with -showInterstitial.
 */
- (void)didClickInterstitial;

/**
 Called with an error occurs when requesting interstitials from the Tapdaq servers.
 */
- (void)didFailToFetchInterstitialsFromServer;

/**
 Called when the request to obtain interstitials from the Tapdaq servers was successful, but no interstitials were found.
 */
- (void)hasNoInterstitialsAvailable;

#pragma mark Video delegate methods

/**
 Called immediately after a video ad is available to the user.
 This method is only used in conjunction with -showVideo.
 */
- (void)didLoadVideo;

/**
 Called immediately after a video is available to the user for a specific placement tag.
 This method should be used in conjunction with -showVideoForPlacementTag:.
 @param tag A placement tag.
 */
- (void)didLoadVideoForPlacementTag:(NSString *)tag;

/**
 Called immediately before the video is to be displayed to the user.
 This method is only used in conjunction with -showVideo.
 */
- (void)willDisplayVideo;

/**
 Called immediately after the video is displayed to the user.
 This method is only used in conjunction with -showVideo.
 */
- (void)didDisplayVideo;

/**
 Called when, for whatever reason, the video was not able to be displayed.
 This method is only used in conjunction with -showVideo.
 */
- (void)didFailToDisplayVideo;

/**
 Called when, for whatever reason, the video was not able to be displayed for a specific network.
 This method is only used in conjunction with -showInterstitial.
 */
- (void)didFailToDisplayVideoForNetwork:(TDMNetwork)network;

/**
 Called when the user closes video.
 This method is only used in conjunction with -showVideo.
 */
- (void)willCloseVideo;

/**
 Called when the user closes the ad shown after a video, either by tapping the close button.
 This method is only used in conjunction with -showVideo.
 */
- (void)willDisplayVideoEndAd;

/**
 Called when the user closes video.
 This method is only used in conjunction with -showVideo.
 */
- (void)didCloseVideo;

/**
 Called when the user clicks the video ad.
 This method is only used in conjunction with -showVideo.
 */
- (void)didClickVideo;


#pragma mark Rewarded Video delegate methods

/**
 Called immediately after a rewarded video ad is available to the user.
 This method is only used in conjunction with -showRewardedVideo.
 */
- (void)didLoadRewardedVideo;

/**
 Called immediately after a rewarded video is available to the user for a specific placement tag.
 This method should be used in conjunction with -showRewardedVideoForPlacementTag:.
 @param tag A placement tag.
 */
- (void)didLoadRewardedVideoForPlacementTag:(NSString *)tag;

/**
 Called when a validation of a reward has succeeded.
 This method is only used in conjunction with -showRewardedVideo.
 */
- (void)rewardValidationSuceeded:(NSString *) rewardName
                    rewardAmount:(int) rewardAmount;

/**
 Called when a validation of a reward has exceeded the quota.
 This method is only used in conjunction with -showRewardedVideo.
 */
- (void)rewardValidationExceededQuota;

/**
 Called when a validation of a reward has been rejected.
 This method is only used in conjunction with -showRewardedVideo.
 */
- (void)rewardValidationRejected;

/**
 Called when a validation of a reward has errored.
 This method is only used in conjunction with -showRewardedVideo.
 */
- (void)rewardValidationErrored;

/**
 Called when a user declines to watch a rewarded video. Applicable if a pop is displayed
 This method is only used in conjunction with -showRewardedVideo.
 */
- (void)userDeclinedToViewRewardedVideo;

#pragma mark Native advert delegate methods

/**
 Called when an error occurs when requesting native adverts from the Tapdaq servers.
 */
- (void)didFailToFetchNativeAdvertsFromServer;

/**
 Called when the request to obtain native adverts from the Tapdaq servers was successful, but no native adverts were found.
 */
- (void)hasNoNativeAdvertsAvailable;

/**
 Called each time a native advert is ready to be fetched.
 By default this method may be called multiple times on application launch, for each supported native ad type.
 
 @param nativeAdType The ad type of the advert ready to be fetched.
 */
- (void)hasNativeAdvertsAvailableForAdType:(TDNativeAdType)nativeAdType;

/**
 Called each time a native advert is ready to be fetched for a particular placement tag.
 
 @param tag The placement tag of the advert ready to be fetched.
 @param nativeAdType The ad type of the advert ready to be fetched.
 */
- (void)hasNativeAdvertsAvailableForPlacementTag:(NSString *)tag adType:(TDNativeAdType)nativeAdType;

#pragma mark Mediation mode delegate methods

/**
 Used only when mediation mode is enabled, see TDProperties.
 Called when an interstitial is successfully loaded, used in conjunction with -loadInterstitialAdvertForOrientation:.
 
 @param advert The loaded interstitial advert.
 @param orientation The orientation of the interstitial advert.
 */
- (void)didLoadInterstitial:(TDInterstitialAdvert *)advert forOrientation:(TDOrientation)orientation;

/**
 Used only when mediation mode is enabled, see TDProperties.
 Called when an interstitial is successfully loaded, used in conjunction with -loadInterstitialAdvertForOrientation:.
 
 @param advert The loaded interstitial advert.
 @param tag The placement tag of the interstitial that loaded.
 @param orientation The orientation of the interstitial that loaded.
 */
- (void)didLoadInterstitial:(TDInterstitialAdvert *)advert forPlacementTag:(NSString *)tag orientation:(TDOrientation)orientation;

/**
 Used only when mediation mode is enabled, see TDProperties.
 Called when the interstitial failed to load, used in conjunction with -loadInterstitialAdvertForOrientation:.
 
 @param orientation The orientation of the interstitial that failed to load.
 */
- (void)didFailToLoadInterstitialForOrientation:(TDOrientation)orientation;

/**
 Used only when mediation mode is enabled, see TDProperties.
 Called when the interstitial failed to load, used in conjunction with -loadInterstitialAdvertForPlacementTag:orientation:.

 @param tag The placement tag of the advert that failed to load.
 @param orientation The orientation of the advert that failed to load.
 */
- (void)didFailToLoadInterstitialForPlacementTag:(NSString *)tag orientation:(TDOrientation)orientation;

/**
 Used only when mediation mode is enabled, see TDProperties.
 Called when a native advert is successfully loaded, used in conjunction with -loadNativeAdvertForAdType:.
 
 @param advert The loaded native advert.
 @param nativeAdType The ad type.
 */
- (void)didLoadNativeAdvert:(TDNativeAdvert *)advert
                  forAdType:(TDNativeAdType)nativeAdType;

/**
 Used only when mediation mode is enabled, see TDProperties.
 Called when a native advert is successfully loaded, used in conjunction with -loadNativeAdvertForPlacementTag:adType:.
 
 @param advert The loaded native advert.
 @param tag The placement tag of the native advert that loaded.
 @param nativeAdType The ad type of the native advert that loaded.
 */
- (void)didLoadNativeAdvert:(TDNativeAdvert *)advert
            forPlacementTag:(NSString *)tag
                     adType:(TDNativeAdType)nativeAdType;

/**
 Used only when mediation mode is enabled, see TDProperties.
 Called when the native ad failed to load, used in conjunction with -loadNativeAdvertForAdType:.
 
 @param nativeAdType The ad type of the native advert that failed to load.
 */
- (void)didFailToLoadNativeAdvertForAdType:(TDNativeAdType)nativeAdType;

/**
 Used only when mediation mode is enabled, see TDProperties.
 Called when the native ad failed to load, used in conjunction with -loadNativeAdvertForPlacementTag:adType:.
 
 @param tag The placement tag that failed to load the native ad.
 @param nativeAdType The ad type of the native advert that failed to load.
 */
- (void)didFailToLoadNativeAdvertForPlacementTag:(NSString *)tag adType:(TDNativeAdType)nativeAdType;

/***************************
 Deprecated delegate methods
 ***************************/

- (void)hasNativeAdvertsAvailableForAdUnit:(TDNativeAdUnit)adUnit
                                      size:(TDNativeAdSize)adSize
                               orientation:(TDOrientation)orientation __deprecated_msg("Use hasNativeAdvertsAvailableForAdType:");

- (void)didLoadNativeAdvert:(TDNativeAdvert *)advert
                  forAdUnit:(TDNativeAdUnit)adUnit
                       size:(TDNativeAdSize)adSize
                orientation:(TDOrientation)orientation __deprecated_msg("Use didLoadNativeAdvert:forAdType:");

- (void)didFailToLoadNativeAdvertForAdUnit:(TDNativeAdUnit)adUnit
                                      size:(TDNativeAdSize)adSize
                               orientation:(TDOrientation)orientation __deprecated_msg("Use didFailToLoadNativeAdvertForAdType:");

- (void)didFailToLoadInterstitial __deprecated_msg("Use didFailToFetchInterstitialsFromServer");

- (void)didFailToShowInterstitial __deprecated_msg("Use didFailToDisplayInterstitial");

- (void)didFailToLoadNativeAdverts __deprecated_msg("Use didFailtoFetchNativeAdvertsFromServer");

- (void)hasInterstitialsAvailableForOrientation:(TDOrientation)orientation __deprecated_msg("Use didLoadInterstitialForOrientation");

- (void)hasInterstitialsAvailableForPlacementTag:(NSString *)tag orientation:(TDOrientation)orientation __deprecated_msg("Use didLoadInterstitialForPlacementTag:orientation");;

@end
