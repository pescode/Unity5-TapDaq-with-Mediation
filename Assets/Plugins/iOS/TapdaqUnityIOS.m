//
//  TapdaqUnityIOS.m
//  TapdaqUnity
//
//  Created by Rheo Violenes on 05/05/15.
//  Copyright (c) 2015 Nerd. All rights reserved.
//
#import "TapdaqUnityIOS.h"

extern UIViewController *UnityGetGLViewController();
extern UIView *UnityGetGLView();

void _GenerateCallBacks(NativeDelegateCallback nativeCallback,
                        InterstitialDelegateCallback interstitialCallback) {
    
    [[TapdaqUnityIOS sharedInstance] associateCallbacksToNativeDelegate:nativeCallback
                                                   interstitialDelegate:interstitialCallback];
    
}

void _ConfigureTapdaq(const char* appIdChar,
                      const char* clientKeyChar,
                      int frequencyCap,
                      int frequencyCapDurationInDays,
                      const char* enabledAdTypesChar,
                      bool testMode) {
    
    NSString *appId = [[NSString stringWithUTF8String:appIdChar] copy];
    NSString *clientKey = [[NSString stringWithUTF8String:clientKeyChar] copy];
    NSString *enabledAdTypes = [[NSString stringWithUTF8String:enabledAdTypesChar] copy];
    
    [[TapdaqUnityIOS sharedInstance] initWithApplicationId:appId
                                                 clientKey:clientKey
                                              frequencyCap:frequencyCap
                                frequencyCapDurationInDays:frequencyCapDurationInDays
                                            enabledAdTypes:enabledAdTypes
                                                  testMode:testMode];
    
}

void _FetchNative(NativeCallback callback,
                  int adTypeInt) {
    
    TDNativeAdType adType = (TDNativeAdType) adTypeInt;
    
    [[TapdaqUnityIOS sharedInstance] fetchNativeForAdType:adType
                                                 callback:callback];
    
}

void _FetchNativeAdWithTag (NativeCallback callback, NSString* tag, int nativeAdType) {
    [[TapdaqUnityIOS sharedInstance] fetchNativeForWithTag:tag
                                                    AdType:(TDNativeAdType)nativeAdType
                                                  callback:callback];
}

void _SendNativeClick(int index) {
    [[TapdaqUnityIOS sharedInstance] triggerClickForNativeAdvertAtIndex:index];
}

void _SendNativeImpression(int index) {
    [[TapdaqUnityIOS sharedInstance] triggerImpressionForNativeAdvertAtIndex:index];
}

void _LaunchMediationDebugger() {
    [[TapdaqUnityIOS sharedInstance] launchMediationDebugger];
}

void _AddTestDevices(const char* adNetwork, const char* deviceIDs) {
    NSString *network = [NSString stringWithUTF8String:adNetwork];
    NSString *dIDs = [NSString stringWithUTF8String:deviceIDs];
    NSArray *IDs = [dIDs componentsSeparatedByString:@"<!@#$%$#@!>"];
    
    [[Tapdaq sharedSession] addTestDevices:(TDMNetwork)network
                             testDeviceIDs:IDs];
}

// interstitial
void _ShowInterstitial(int* _orientation) {
    NSLog(@"[TapdaqUnityiOS] showInterstitial called");
    [[TapdaqUnityIOS sharedInstance] showInterstitial];
}

void _ShowInterstitialWithTag(const char* tagChar) {
    NSString *tag = [NSString stringWithUTF8String:tagChar];
    [[Tapdaq sharedSession] showInterstitialForPlacementTag:tag];
}

// banner
void _RequestBanner (int size) {
    [[TapdaqUnityIOS sharedInstance] requestBanner:(TDMBannerSize)size];
}

void _RequestBannerWithTag (const char* tagChar, int size) {
    NSString *tag = [NSString stringWithUTF8String:tagChar];
    [[Tapdaq sharedSession] loadBannerForPlacementTag:(NSString *)tag
                                                 size:(TDMBannerSize)size];
}

void _ShowBanner () {
    NSLog(@"[TapdaqUnityiOS] showBanner called");
    [[TapdaqUnityIOS sharedInstance] showBanner];
}

void _ShowBannerWithTag (const char* tagChar) {
    NSString *tag = [NSString stringWithUTF8String:tagChar];
    [[Tapdaq sharedSession] showBannerForPlacementTag:(NSString *)tag];
}

// video
void _ShowVideo () {
    NSLog(@"[TapdaqUnityiOS] showVideo called");
    [[TapdaqUnityIOS sharedInstance] showVideo];
}

void _ShowVideoWithTag (const char* tagChar) {
    NSString *tag = [NSString stringWithUTF8String:tagChar];
    [[Tapdaq sharedSession] showVideoForPlacementTag:(NSString *)tag];
}

// reward video
void _ShowRewardVideo () {
    NSLog(@"[TapdaqUnityiOS] showRewardedVideo called");
    [[TapdaqUnityIOS sharedInstance] showRewardVideo];
}

void _ShowRewardVideoWithTag (const char* tagChar) {
    NSString *tag = [NSString stringWithUTF8String:tagChar];
    [[Tapdaq sharedSession] showRewardedVideoForPlacementTag:(NSString *)tag];
}

@implementation TapdaqUnityIOS

+ (instancetype)sharedInstance
{
    static dispatch_once_t once;
    static id sharedInstance;
    dispatch_once(&once, ^{
        sharedInstance = [[self alloc] init];
    });
    return sharedInstance;
}

+ (void)load
{
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(notifyOrientationChange:) name:UIApplicationDidChangeStatusBarOrientationNotification object:nil];
    
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(notifyApplicationEnterBackground:) name:UIApplicationDidEnterBackgroundNotification object:nil];
    
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(createPlugin:) name:UIApplicationDidFinishLaunchingNotification object:nil];
}

+ (void)createPlugin:(NSNotification *)notification
{
    [TapdaqUnityIOS sharedInstance];
}

+ (void)notifyOrientationChange:(NSNotification *)notification
{
    //notify unity of orientational changes
    UnitySendMessage("TapdaqV1","_orientationChangeNotification",[@"" UTF8String]);
}

+ (void)notifyApplicationEnterBackground:(NSNotification *)notification
{
    UnitySendMessage("TapdaqV1","_applicationEnterBackgroundNotification",[@"" UTF8String]);
}


-(void)retainer
{
    //Work around for Unity5 delegate bug
}

- (void)associateCallbacksToNativeDelegate:(NativeDelegateCallback)nativeCallback
                      interstitialDelegate:(InterstitialDelegateCallback)interstitialCallback
{
    self.NativeDelegateCallBackObj = nativeCallback;
    self.InterstitialDelegateCallbackObj = interstitialCallback;
}


// Init
//Configure Tapdaq with credentials and ad settings
- (void)initWithApplicationId:(NSString *)appID
                    clientKey:(NSString *)clientKey
                 frequencyCap:(int)frequencyCap
   frequencyCapDurationInDays:(int)durationInDays
               enabledAdTypes:(NSString *)enabledAdTypes
                     testMode:(BOOL)testMode
{
    
    TDProperties *properties = [[TDProperties alloc] init];
    properties.frequencyCap = frequencyCap;
    properties.frequencyDurationInDays = durationInDays;
    
    if (![enabledAdTypes isEqual:@""]) {
        
        NSDictionary *validAdTypes = @{
                                       
                                       @"TDAdTypeNone": @(0),
                                       @"TDAdTypeInterstitial": @(1),
                                       @"TDAdType1x1Large": @(2),
                                       @"TDAdType1x1Medium": @(3),
                                       @"TDAdType1x1Small": @(4),
                                       
                                       @"TDAdType1x2Large": @(5),
                                       @"TDAdType1x2Medium": @(6),
                                       @"TDAdType1x2Small": @(7),
                                       
                                       @"TDAdType2x1Large": @(8),
                                       @"TDAdType2x1Medium": @(9),
                                       @"TDAdType2x1Small": @(10),
                                       
                                       @"TDAdType2x3Large": @(11),
                                       @"TDAdType2x3Medium": @(12),
                                       @"TDAdType2x3Small": @(13),
                                       
                                       @"TDAdType3x2Large": @(14),
                                       @"TDAdType3x2Medium": @(15),
                                       @"TDAdType3x2Small": @(16),
                                       
                                       @"TDAdType1x5Large": @(17),
                                       @"TDAdType1x5Medium": @(18),
                                       @"TDAdType1x5Small": @(19),
                                       
                                       @"TDAdType5x1Large": @(20),
                                       @"TDAdType5x1Medium": @(21),
                                       @"TDAdType5x1Small": @(22),
                                       
                                       @"TDAdTypeVideo": @(23),
                                       @"TDAdTypeRewardedVideo": @(24),
                                       @"TDAdTypeBanner": @(25)
                                       
                                       };
        
        NSMutableDictionary *tagsWithAdTypes = [[NSMutableDictionary alloc] init];
        
        if (enabledAdTypes != nil) {
            NSArray *tempAdTypes = [enabledAdTypes componentsSeparatedByString:@";"];
            
            if ([tempAdTypes count] > 0) {
                for (NSString *tempAdType in tempAdTypes) {
                    
                    NSArray *tempAdInfo = [tempAdType componentsSeparatedByString:@"-"];
                    
                    if ([tempAdInfo count] == 2) {
                        
                        NSString *adTypeString = [tempAdInfo objectAtIndex:0];
                        NSString *commaSeparatedTagString = [tempAdInfo objectAtIndex:1];
                        
                        NSArray *tags = [commaSeparatedTagString componentsSeparatedByString:@","];
                        
                        if ([tags count] > 0) {
                            for (NSString *tag in tags) {
                                
                                // update tagsWithAdTypes
                                NSNumber *combinedAdTypeNum = [tagsWithAdTypes objectForKey:tag];
                                
                                if (!combinedAdTypeNum) {
                                    combinedAdTypeNum = @(0);
                                }
                                
                                TDAdTypes adTypesCombined = [combinedAdTypeNum integerValue];
                                
                                NSNumber *adTypeNum = [validAdTypes objectForKey:adTypeString];
                                NSInteger adTypeInt = [adTypeNum integerValue];
                                
                                adTypesCombined |= 1 << adTypeInt;
                                
                                combinedAdTypeNum = @(adTypesCombined);
                                
                                [tagsWithAdTypes setObject:combinedAdTypeNum forKey:tag];
                                
                            }
                        }
                        
                    }
                    
                }
            }
        }
        
        
        for (id key in tagsWithAdTypes) {
            
            if ([key isKindOfClass:[NSString class]] && [[tagsWithAdTypes objectForKey:key] integerValue] > 0) {
                NSString *tag = (NSString *) key;
                TDAdTypes adTypes = (TDAdTypes) [[tagsWithAdTypes objectForKey:key] integerValue];
                
                if (tag && [tag length] > 0) {
                    TDPlacement *placement = [[TDPlacement alloc] initWithAdTypes:adTypes forTag:tag];
                    NSLog(@"%@", placement);
                    [properties registerPlacement:placement];
                }
                
            }
            
        }
        
    }
    
    [[Tapdaq sharedSession] setApplicationId:appID
                                   clientKey:clientKey
                                  properties:properties];
    
    [(Tapdaq *)[Tapdaq sharedSession] setDelegate:self];
    
    
    [[Tapdaq sharedSession] launch];
    
    
    self.nativePointer = 0;
    
    self.nativeAdList = [[NSMutableArray alloc] init];
    
    NSLog(@"Tapdaq Configured");
    [self performSelector:@selector(retainer) withObject:nil afterDelay:100.0];
}

- (void)fetchNativeForAdType:(TDNativeAdType)adType
                    callback:(NativeCallback)callback
{
    
    TDNativeAdvert *nativeAdvert = [[Tapdaq sharedSession] getNativeAdvertForAdType:adType];
    
    NSLog(@"Native advert: %@", nativeAdvert);
    
    if (nativeAdvert != nil) {
        
        CGFloat aspectRatioWidth = [nativeAdvert.creative.aspectRatio width];
        CGFloat aspectRatioHeight = [nativeAdvert.creative.aspectRatio width];
        
        NSData *imageData = UIImagePNGRepresentation(nativeAdvert.creative.image);
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
        NSString *documentsDirectory = [paths objectAtIndex:0];
        NSString *filePath = [documentsDirectory stringByAppendingPathComponent:@"selected2.png"]; //Add the file name
        [imageData writeToFile:filePath atomically:YES];
        
        imageData = UIImagePNGRepresentation(nativeAdvert.icon);
        NSString *iconPath = [documentsDirectory stringByAppendingPathComponent:@"iconSelected2.png"]; //Add the file name
        [imageData writeToFile:iconPath atomically:YES];
        
        NSString *nativeAdObject = [NSString stringWithFormat:@"%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@",
                                    nativeAdvert.applicationId,
                                    nativeAdvert.targetingId,
                                    nativeAdvert.subscriptionId,
                                    nativeAdvert.appName,
                                    nativeAdvert.appDescription,
                                    nativeAdvert.buttonText,
                                    nativeAdvert.developerName,
                                    nativeAdvert.ageRating,
                                    [[NSNumber numberWithFloat:nativeAdvert.appSize] stringValue],
                                    [[NSNumber numberWithFloat:nativeAdvert.averageReview] stringValue],
                                    [[NSNumber numberWithInt:nativeAdvert.totalReviews] stringValue],
                                    nativeAdvert.category,
                                    nativeAdvert.appVersion,
                                    [[NSNumber numberWithFloat:nativeAdvert.price] stringValue],
                                    nativeAdvert.currency,
                                    [[NSNumber numberWithUnsignedInt:(uint)nativeAdvert.adUnit] stringValue],
                                    [[NSNumber numberWithUnsignedInt:(uint)nativeAdvert.adSize] stringValue],
                                    [nativeAdvert.iconUrl absoluteString],
                                    iconPath,
                                    nativeAdvert.creative.identifier,
                                    [[NSNumber numberWithUnsignedInt:(uint)nativeAdvert.creative.orientation] stringValue],
                                    [[NSNumber numberWithUnsignedInt:(uint)nativeAdvert.creative.resolution] stringValue],
                                    [[NSNumber numberWithFloat:aspectRatioWidth] stringValue],
                                    [[NSNumber numberWithFloat:aspectRatioHeight] stringValue],
                                    [nativeAdvert.creative.url absoluteString],
                                    filePath,
                                    [[NSNumber numberWithInt:self.nativePointer] stringValue]
                                    ];
        
        self.nativePointer++;
        [self.nativeAdList addObject:nativeAdvert];
        callback([nativeAdObject UTF8String]);
    }
    else
    {
        NSLog(@"No nativeAd available");
        UnitySendMessage("TapdaqV1","FetchFailed",[@"" UTF8String]);
    }
    
}

- (void)fetchNativeForWithTag:(NSString *)tag
                       AdType:(TDNativeAdType)adType
                     callback:(NativeCallback)callback
{
    
    TDNativeAdvert *nativeAdvert = [[Tapdaq sharedSession] getNativeAdvertForPlacementTag:tag
                                                                                   adType:adType];
    
    NSLog(@"Native advert: %@", nativeAdvert);
    
    if (nativeAdvert != nil) {
        
        CGFloat aspectRatioWidth = [nativeAdvert.creative.aspectRatio width];
        CGFloat aspectRatioHeight = [nativeAdvert.creative.aspectRatio width];
        
        NSData *imageData = UIImagePNGRepresentation(nativeAdvert.creative.image);
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
        NSString *documentsDirectory = [paths objectAtIndex:0];
        NSString *filePath = [documentsDirectory stringByAppendingPathComponent:@"selected2.png"]; //Add the file name
        [imageData writeToFile:filePath atomically:YES];
        
        imageData = UIImagePNGRepresentation(nativeAdvert.icon);
        NSString *iconPath = [documentsDirectory stringByAppendingPathComponent:@"iconSelected2.png"]; //Add the file name
        [imageData writeToFile:iconPath atomically:YES];
        
        NSString *nativeAdObject = [NSString stringWithFormat:@"%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@<>%@",
                                    nativeAdvert.applicationId,
                                    nativeAdvert.targetingId,
                                    nativeAdvert.subscriptionId,
                                    nativeAdvert.appName,
                                    nativeAdvert.appDescription,
                                    nativeAdvert.buttonText,
                                    nativeAdvert.developerName,
                                    nativeAdvert.ageRating,
                                    [[NSNumber numberWithFloat:nativeAdvert.appSize] stringValue],
                                    [[NSNumber numberWithFloat:nativeAdvert.averageReview] stringValue],
                                    [[NSNumber numberWithInt:nativeAdvert.totalReviews] stringValue],
                                    nativeAdvert.category,
                                    nativeAdvert.appVersion,
                                    [[NSNumber numberWithFloat:nativeAdvert.price] stringValue],
                                    nativeAdvert.currency,
                                    [[NSNumber numberWithUnsignedInt:(uint)nativeAdvert.adUnit] stringValue],
                                    [[NSNumber numberWithUnsignedInt:(uint)nativeAdvert.adSize] stringValue],
                                    [nativeAdvert.iconUrl absoluteString],
                                    iconPath,
                                    nativeAdvert.creative.identifier,
                                    [[NSNumber numberWithUnsignedInt:(uint)nativeAdvert.creative.orientation] stringValue],
                                    [[NSNumber numberWithUnsignedInt:(uint)nativeAdvert.creative.resolution] stringValue],
                                    [[NSNumber numberWithFloat:aspectRatioWidth] stringValue],
                                    [[NSNumber numberWithFloat:aspectRatioHeight] stringValue],
                                    [nativeAdvert.creative.url absoluteString],
                                    filePath,
                                    [[NSNumber numberWithInt:self.nativePointer] stringValue]
                                    ];
        
        self.nativePointer++;
        [self.nativeAdList addObject:nativeAdvert];
        callback([nativeAdObject UTF8String]);
    }
    else
    {
        NSLog(@"No nativeAd available");
        UnitySendMessage("TapdaqV1","FetchFailed",[@"" UTF8String]);
    }
    
}

- (void)triggerClickForNativeAdvertAtIndex:(int)index
{
    NSUInteger count = [self.nativeAdList count];
    
    if (count > 0 && index <= (count - 1)) {
        TDNativeAdvert *nativeAdvert = [self.nativeAdList objectAtIndex:index];
        
        if (nativeAdvert != nil) {
            [nativeAdvert triggerClick];
        }
    }
}

-(void)triggerImpressionForNativeAdvertAtIndex:(int)index
{
    NSUInteger count = [self.nativeAdList count];
    if (count > 0 && index <= (count - 1)) {
        TDNativeAdvert *nativeAdvert = [self.nativeAdList objectAtIndex:index];
        if (nativeAdvert != nil) {
            [nativeAdvert triggerImpression];
        }
    }
}

- (void)launchMediationDebugger
{
    //Launch integration tester
    [[Tapdaq sharedSession] launchMediationDebugger:UnityGetGLViewController().parentViewController];
}

// Interstitial

- (void)showInterstitial
{
    [[Tapdaq sharedSession] showInterstitial];
}

// Banner

- (void)requestBanner:(TDMBannerSize)size
{
    //TDMBannerStandard,
    //TDMBannerLarge,
    //TDMBannerMedium,
    //TDMBannerFull,
    //TDMBannerLeaderboard,
    //TDMBannerSmartPortrait,
    //TDMBannerSmartLandscape
    
    //Load a banner ad
    [[Tapdaq sharedSession] loadBanner:size];
}

- (void)showBanner
{
    //Define a view for the banner
    UIView *bannerView;
    
    //Get the banner ad
    bannerView = [[Tapdaq sharedSession] showBanner];
    
    //Place it at the bottom
    bannerView.frame = CGRectMake((UnityGetGLView().frame.size.width-bannerView.frame.size.width)/2, UnityGetGLView().frame.size.height-bannerView.frame.size.height, bannerView.frame.size.width, bannerView.frame.size.height);
    
    //Add it to your view
    [UnityGetGLView() addSubview:bannerView];
}

// Video

- (void)showVideo
{
    //Show the video ad
    [[Tapdaq sharedSession] showVideo];
}

// Reward Video

- (void)showRewardVideo
{
    //Show the rewarded video ad
    [[Tapdaq sharedSession] showRewardedVideo];
}



//
// Delegates
//


// Interstitial

// If you would like run some code right after an interstitial is loaded, implement the following:
- (void)didLoadInterstitial
{
    UnitySendMessage("TapdaqV1","_didLoadInterstitial",[@"" UTF8String]);
}

// If you would like run some code before an interstitial is shown, implement the following:
- (void)willDisplayInterstitial
{
    UnitySendMessage("TapdaqV1","_willDisplayInterstitial",[@"" UTF8String]);
}

// If you would like to run some code after an interstitial is shown, implement the following:
- (void)didDisplayInterstitial
{
    UnitySendMessage("TapdaqV1","_didDisplayInterstitial",[@"" UTF8String]);
}

// If you would like to run some code just before an interstitial is closed, implement the following:
- (void)willCloseInterstitial
{
    UnitySendMessage("TapdaqV1","_willCloseInterstitial",[@"" UTF8String]);
}

// If you would like to run some code when an interstitial is closed, implement the following:
- (void)didCloseInterstitial
{
    UnitySendMessage("TapdaqV1","_didCloseInterstitial",[@"" UTF8String]);
}

// If you would like to run some code when an interstitial is clicked, implement the following:
- (void)didClickInterstitial
{
    UnitySendMessage("TapdaqV1","_didClickInterstitial",[@"" UTF8String]);
}

// When an interstitial fails to show
- (void)didFailToDisplayInterstitial
{
    UnitySendMessage("TapdaqV1","_didFailToDisplayInterstitial",[@"" UTF8String]);
}

// When error occurs requesting interstitials from the servers
- (void)didFailToFetchInterstitialsFromServer
{
    UnitySendMessage("TapdaqV1","_didFailToLoadInterstitial",[@"" UTF8String]);
}

// When servers respond back with empty queue of interstitials
- (void)hasNoInterstitialsAvailable
{
    UnitySendMessage("TapdaqV1","_hasNoInterstitialsAvailable",[@"" UTF8String]);
}

// When an interstitial is ready to display
- (void)hasInterstitialsAvailableForOrientation:(TDOrientation)orientation
{
    self.InterstitialDelegateCallbackObj([[[NSNumber numberWithUnsignedInt:(uint)orientation] stringValue] UTF8String]);
}

// OLDER VERSION DELEGATES

// When interstitial fails to load
- (void)didFailToLoadInterstitialForOrientation:(TDOrientation)orientation
{
    UnitySendMessage("TapdaqV1","_didFailToLoadInterstitialForOrientation",[[[NSNumber numberWithUnsignedInt:(uint)orientation] stringValue] UTF8String]);
}



// Banner

// Called immediately after the banner is loaded
- (void)didLoadBanner
{
    UnitySendMessage("TapdaqV1","_didLoadBanner",[@"" UTF8String]);
}

// Called immediately before the banner is to be displayed to the user
- (void)willDisplayBanner
{
    UnitySendMessage("TapdaqV1","_willDisplayBanner",[@"" UTF8String]);
}

// Called immediately after the banner is displayed to the user
- (void)didDisplayBanner
{
    UnitySendMessage("TapdaqV1","_didDisplayBanner",[@"" UTF8String]);
}

// Called when, for whatever reason, the banner was not able to be displayed
- (void)didFailToDisplayBanner
{
    UnitySendMessage("TapdaqV1","_didFailToDisplayBanner",[@"" UTF8String]);
}

// Called when the user clicks the banner
- (void)didClickBanner
{
    UnitySendMessage("TapdaqV1","_didClickBanner",[@"" UTF8String]);
}

// Called when the user clicked banner ad has finished and the user is returned to the app
- (void)didFinishHandlingClickBanner
{
    UnitySendMessage("TapdaqV1","_didFinishHandlingClickBanner",[@"" UTF8String]);
}



// Video

// Called immediately after a video ad is available to the user
- (void)didLoadVideo
{
    UnitySendMessage("TapdaqV1","_didLoadVideo",[@"" UTF8String]);
}

// Called immediately before the video is to be displayed to the user
- (void)willDisplayVideo
{
    UnitySendMessage("TapdaqV1","_willDisplayVideo",[@"" UTF8String]);
}

// Called immediately after the video is displayed to the user
- (void)didDisplayVideo
{
    UnitySendMessage("TapdaqV1","_didDisplayVideo",[@"" UTF8String]);
}

// Called when, for whatever reason, the video was not able to be displayed
- (void)didFailToDisplayVideo
{
    UnitySendMessage("TapdaqV1","_didFailToDisplayVideo",[@"" UTF8String]);
}

// Called when the user closes video
- (void)willCloseVideo
{
    UnitySendMessage("TapdaqV1","_willCloseVideo",[@"" UTF8String]);
}

// Called when the user closes the ad shown after a video, either by tapping the close button
- (void)willDisplayVideoEndAd
{
    UnitySendMessage("TapdaqV1","_willDisplayVideoEndAd",[@"" UTF8String]);
}

// Called when the user closes video
- (void)didCloseVideo
{
    UnitySendMessage("TapdaqV1","_didCloseVideo",[@"" UTF8String]);
}

// Called when the user clicks the video ad
- (void)didClickVideo
{
    UnitySendMessage("TapdaqV1","_didClickVideo",[@"" UTF8String]);
}



// Reward Video

// Called immediately after a rewarded video ad is available to the user
- (void)didLoadRewardedVideo
{
    UnitySendMessage("TapdaqV1","_didLoadRewardedVideo",[@"" UTF8String]);
}

// Called when a validation of a reward has succeeded
- (void)rewardValidationSuceeded:(NSString *) rewardName
                    rewardAmount:(int) rewardAmount
{
    UnitySendMessage("TapdaqV1","_rewardValidationSuceeded",[rewardName UTF8String]);
}

// Called when a validation of a reward has exceeded the quota
- (void)rewardValidationExceededQuota
{
    UnitySendMessage("TapdaqV1","_rewardValidationExceededQuota",[@"" UTF8String]);
}

// Called when a validation of a reward has been rejected
- (void)rewardValidationRejected
{
    UnitySendMessage("TapdaqV1","_rewardValidationRejected",[@"" UTF8String]);
}

// Called when a validation of a reward has errored
- (void)rewardValidationErrored
{
    UnitySendMessage("TapdaqV1","_rewardValidationErrored",[@"" UTF8String]);
}

// Called when a user declines to watch a rewarded video. Applicable if a pop is displayed
- (void)userDeclinedToViewRewardedVideo
{
    UnitySendMessage("TapdaqV1","_userDeclinedToViewRewardedVideo",[@"" UTF8String]);
}



// Native

// When error occurs requesting native adverts from the servers
- (void)didFailToFetchNativeAdvertsFromServer
{
    UnitySendMessage("TapdaqV1","_didFailToLoadNativeAdverts",[@"" UTF8String]);
}

// When servers respond back with empty queue of native adverts
- (void)hasNoNativeAdvertsAvailable
{
    UnitySendMessage("TapdaqV1","_hasNoNativeAdvertsAvailable",[@"" UTF8String]);
}

// When a native advert is ready to display
- (void)hasNativeAdvertsAvailableForAdType:(TDNativeAdType)nativeAdType
{
    self.NativeDelegateCallBackObj ([[[NSNumber numberWithUnsignedInt:(uint)nativeAdType] stringValue] UTF8String]);
}

// OLDER VERSION DELEGATES

// When native advert is successfully loaded
- (void)didLoadNativeAdvert:(TDNativeAdvert *)advert
                  forAdType:(TDNativeAdType)nativeAdType
{
    UnitySendMessage("TapdaqV1","_didLoadNativeAdvert",[[[NSNumber numberWithUnsignedInt:(uint)nativeAdType] stringValue] UTF8String]);
}

// When native advert fails to load
- (void)didFailToLoadNativeAdvertForAdType:(TDNativeAdType)nativeAdType
{
    UnitySendMessage("TapdaqV1","_didFailToLoadNativeAdvertForAdType",[[[NSNumber numberWithUnsignedInt:(uint)nativeAdType] stringValue] UTF8String]);
}
@end
