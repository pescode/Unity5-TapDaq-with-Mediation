//
//  AdNetworkDebugViewController.m
//  Tapdaq iOS SDK
//
//  Created by Mukund Agarwal on 01/09/2016.
//  Copyright © 2016 Tapdaq. All rights reserved.
//

#import "AdNetworkDebugViewController.h"

//#import "TDMConstants.h"
//#import "TDMKeys.h"
//#import "TDMRequestManager.h"

@interface AdNetworkDebugViewController ()

@end

@implementation AdNetworkDebugViewController

UIView *bannerView;
NSInteger debugLines = 0;

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
    
    [self updateVersionLabel];
    
    self.navigationItem.title = self.adNetwork;
    
    //Set the ad types
    if ([self.adNetwork isEqualToString:@"AdColony"])
    {
        [self.adType setEnabled:NO forSegmentAtIndex:0];    //Interstitial
        [self.adType setEnabled:YES forSegmentAtIndex:1];   //Video
        [self.adType setEnabled:YES forSegmentAtIndex:2];   //Rewarded
        [self.adType setEnabled:NO forSegmentAtIndex:3];    //Banner
        
        [self.adType setSelectedSegmentIndex:1];
    }
    else if ([self.adNetwork isEqualToString:@"AdMob"])
    {
        [self.adType setEnabled:YES forSegmentAtIndex:0];
        [self.adType setEnabled:YES forSegmentAtIndex:1];
        [self.adType setEnabled:NO forSegmentAtIndex:2];
        [self.adType setEnabled:YES forSegmentAtIndex:3];
        
        [self.adType setSelectedSegmentIndex:0];
    }
    else if ([self.adNetwork isEqualToString:@"AppLovin"])
    {
        [self.adType setEnabled:YES forSegmentAtIndex:0];
        [self.adType setEnabled:YES forSegmentAtIndex:1];
        [self.adType setEnabled:YES forSegmentAtIndex:2];
        [self.adType setEnabled:NO forSegmentAtIndex:3];
        
        [self.adType setSelectedSegmentIndex:0];
    }
    else if ([self.adNetwork isEqualToString:@"Chartboost"])
    {
        [self.adType setEnabled:YES forSegmentAtIndex:0];
        [self.adType setEnabled:YES forSegmentAtIndex:1];
        [self.adType setEnabled:YES forSegmentAtIndex:2];
        [self.adType setEnabled:NO forSegmentAtIndex:3];
        
        [self.adType setSelectedSegmentIndex:0];
    }
    else if ([self.adNetwork isEqualToString:@"Facebook"])
    {
        [self.adType setEnabled:YES forSegmentAtIndex:0];
        [self.adType setEnabled:NO forSegmentAtIndex:1];
        [self.adType setEnabled:NO forSegmentAtIndex:2];
        [self.adType setEnabled:YES forSegmentAtIndex:3];
        
        [self.adType setSelectedSegmentIndex:0];
    }
    else if ([self.adNetwork isEqualToString:@"InMobi"])
    {
        [self.adType setEnabled:YES forSegmentAtIndex:0];
        [self.adType setEnabled:YES forSegmentAtIndex:1];
        [self.adType setEnabled:YES forSegmentAtIndex:2];
        [self.adType setEnabled:YES forSegmentAtIndex:3];
        
        [self.adType setSelectedSegmentIndex:0];
    }
    else if ([self.adNetwork isEqualToString:@"UnityAds"])
    {
        [self.adType setEnabled:NO forSegmentAtIndex:0];
        [self.adType setEnabled:YES forSegmentAtIndex:1];
        [self.adType setEnabled:YES forSegmentAtIndex:2];
        [self.adType setEnabled:NO forSegmentAtIndex:3];
        
        [self.adType setSelectedSegmentIndex:1];
    }
    else if ([self.adNetwork isEqualToString:@"Vungle"])
    {
        [self.adType setEnabled:NO forSegmentAtIndex:0];
        [self.adType setEnabled:YES forSegmentAtIndex:1];
        [self.adType setEnabled:YES forSegmentAtIndex:2];
        [self.adType setEnabled:NO forSegmentAtIndex:3];
        
        [self.adType setSelectedSegmentIndex:1];
    }
    else if ([self.adNetwork isEqualToString:@"Mediated"])
    {
        [self.adType setEnabled:YES forSegmentAtIndex:0];
        [self.adType setEnabled:YES forSegmentAtIndex:1];
        [self.adType setEnabled:YES forSegmentAtIndex:2];
        [self.adType setEnabled:YES forSegmentAtIndex:3];
        
        [self.adType setSelectedSegmentIndex:0];
        
        [self.fetchAd setHidden:true];
        
        //Preload banner only if in mediation test mode
        [bannerView removeFromSuperview];
        [[Tapdaq sharedSession] loadBanner:TDMBannerStandard];
        
    }
    
    [self.fetchAd setEnabled:true];
    [self.showAd setEnabled:true];
    
    //Set delegate to update the debug view
    [[Tapdaq sharedSession] setDelegate:self];
    
    [self updateDebugInfo];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)updateVersionLabel{
    [self.versionLabel setText:[NSString stringWithFormat:@"Version %@ (%@)", [[NSBundle mainBundle] objectForInfoDictionaryKey:@"CFBundleShortVersionString"], [[NSBundle mainBundle] objectForInfoDictionaryKey:@"CFBundleVersion"]]];
}

- (void)updateDebugInfo{
    
    bool errorSettingUp = false;
    
    NSBundle *bundle = [NSBundle bundleForClass:[self class]];
    
    UIImage *workingImage = [UIImage imageNamed:@"Working" inBundle:bundle compatibleWithTraitCollection:nil];
    
    if ([self.adNetwork isEqualToString:@"AdColony"])
    {
        if ([[Tapdaq sharedSession] usingNetwork:TDMAdColony]) {
            [self.credentialsDownloadedLabel setText:@"Credentials downloaded"];
            [self.credentialsDownloadedImage setImage:workingImage forState:UIControlStateNormal];

            if ([[Tapdaq sharedSession] usingOfficialAdapter:TDMAdColony]){
                [self.usingAdapterLabel setText:@"Using AdColony adapter"];
            }else{
                [self.usingAdapterLabel setText:@"Using Tapdaq adapter"];
            }
            
            [self.usingAdapterImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedLabel setText:@"Initialised with credentials"];
        }
    }
    else if ([self.adNetwork isEqualToString:@"AdMob"])
    {
        if ([[Tapdaq sharedSession] usingNetwork:TDMAdMob]) {
            [self.credentialsDownloadedLabel setText:@"Credentials downloaded"];
            [self.credentialsDownloadedImage setImage:workingImage forState:UIControlStateNormal];
           
            if ([[Tapdaq sharedSession] usingOfficialAdapter:TDMAdMob]){
                [self.usingAdapterLabel setText:@"Using AdMob adapter"];
            }else{
                [self.usingAdapterLabel setText:@"No Tapdaq adapter found"];
            }

            [self.usingAdapterImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedLabel setText:@"Initialised with credentials"];
        }
    }
    else if ([self.adNetwork isEqualToString:@"AppLovin"])
    {
        if ([[Tapdaq sharedSession] usingNetwork:TDMApplovin]) {
            [self.credentialsDownloadedLabel setText:@"Credentials downloaded"];
            [self.credentialsDownloadedImage setImage:workingImage forState:UIControlStateNormal];
            
            if ([[Tapdaq sharedSession] usingOfficialAdapter:TDMApplovin]){
                [self.usingAdapterLabel setText:@"Using AppLovin adapter"];
            }else{
                [self.usingAdapterLabel setText:@"Using Tapdaq adapter"];
            }
            
            [self.usingAdapterImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedLabel setText:@"Initialised with credentials"];
        }
    }
    else if ([self.adNetwork isEqualToString:@"Chartboost"])
    {
        if ([[Tapdaq sharedSession] usingNetwork:TDMChartboost]) {
            [self.credentialsDownloadedLabel setText:@"Credentials downloaded"];
            [self.credentialsDownloadedImage setImage:workingImage forState:UIControlStateNormal];
            
            if ([[Tapdaq sharedSession] usingOfficialAdapter:TDMChartboost]){
                [self.usingAdapterLabel setText:@"Using Chartboost adapter"];
            }else{
                [self.usingAdapterLabel setText:@"Using Tapdaq adapter"];
            }
            
            [self.usingAdapterImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedLabel setText:@"Initialised with credentials"];
        }
    }
    else if ([self.adNetwork isEqualToString:@"Facebook"])
    {
        if ([[Tapdaq sharedSession] usingNetwork:TDMFacebookAudienceNetwork]) {
            [self.credentialsDownloadedLabel setText:@"Credentials downloaded"];
            [self.credentialsDownloadedImage setImage:workingImage forState:UIControlStateNormal];
            
            if ([[Tapdaq sharedSession] usingOfficialAdapter:TDMFacebookAudienceNetwork]){
                [self.usingAdapterLabel setText:@"Using Facebook adapter"];
            }else{
                [self.usingAdapterLabel setText:@"No Tapdaq adapter found"];
            }
            
            [self.usingAdapterImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedLabel setText:@"Initialised with credentials"];
        }
    }
    else if ([self.adNetwork isEqualToString:@"InMobi"])
    {
        if ([[Tapdaq sharedSession] usingNetwork:TDMInMobi]) {
            [self.credentialsDownloadedLabel setText:@"Credentials downloaded"];
            [self.credentialsDownloadedImage setImage:workingImage forState:UIControlStateNormal];
            
            if ([[Tapdaq sharedSession] usingOfficialAdapter:TDMInMobi]){
                [self.usingAdapterLabel setText:@"Using InMobi adapter"];
            }else{
                [self.usingAdapterLabel setText:@"Using Tapdaq S2S adapter"];
            }
  
            [self.usingAdapterImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedLabel setText:@"Initialised with credentials"];
        }
    }
    else if ([self.adNetwork isEqualToString:@"UnityAds"])
    {
        if ([[Tapdaq sharedSession] usingNetwork:TDMUnityAds]) {
            [self.credentialsDownloadedLabel setText:@"Credentials downloaded"];
            [self.credentialsDownloadedImage setImage:workingImage forState:UIControlStateNormal];
            
            if ([[Tapdaq sharedSession] usingOfficialAdapter:TDMUnityAds]){
                [self.usingAdapterLabel setText:@"Using UnityAds adapter"];
            }else{
                [self.usingAdapterLabel setText:@"Using Tapdaq adapter"];
                
            }
            
            [self.usingAdapterImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedLabel setText:@"Initialised with credentials"];
        }
    }
    else if ([self.adNetwork isEqualToString:@"Vungle"])
    {
        if ([[Tapdaq sharedSession] usingNetwork:TDMVungle]) {
            [self.credentialsDownloadedLabel setText:@"Credentials downloaded"];
            
            [self.credentialsDownloadedImage setImage:workingImage forState:UIControlStateNormal];
            
            if ([[Tapdaq sharedSession] usingOfficialAdapter:TDMVungle]){
                [self.usingAdapterLabel setText:@"Using Vungle adapter"];
            }else{
                [self.usingAdapterLabel setText:@"Using Tapdaq adapter"];
            }
            
            [self.usingAdapterImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedLabel setText:@"Initialised with credentials"];
        }
    }
    else if ([self.adNetwork isEqualToString:@"Mediated"])
    {
        if ([[Tapdaq sharedSession] launchRequestCompleted])
        {
            [self.credentialsDownloadedLabel setText:@"Multiple credentials downloaded"];
            
            [self.credentialsDownloadedImage setImage:workingImage forState:UIControlStateNormal];
            [self.usingAdapterLabel setText:@"Multiple adapter combination"];
            [self.usingAdapterImage setImage:workingImage forState:UIControlStateNormal];
            [self.initialisedImage setImage:workingImage forState:UIControlStateNormal];
            
            [self.initialisedLabel setText:@"Initialised with credentials"];
        }
    }
    
    if (errorSettingUp) {
        [self.adType setEnabled:NO forSegmentAtIndex:0];
        [self.adType setEnabled:NO forSegmentAtIndex:1];
        [self.adType setEnabled:NO forSegmentAtIndex:2];
        [self.adType setEnabled:NO forSegmentAtIndex:3];
        
        [self.fetchAd setEnabled:false];
        [self.showAd setEnabled:false];
        
    }
}

#pragma mark -
#pragma mark UI functions

- (IBAction)fetchingAd:(id)sender {
    TDMDebugLog(@"fetchingAd");
    
    //Check what's selected
    if ([self.adNetwork isEqualToString:@"Mediated"]){
        switch (self.adType.selectedSegmentIndex) {
            case 0:
                //Interstitial
                [[Tapdaq sharedSession] loadInterstitial];
                break;
            case 1:
                //Video
                [[Tapdaq sharedSession] loadVideo];
                break;
            case 2:
                //Rewarded
                [[Tapdaq sharedSession] loadRewardedVideo];
                break;
            case 3:
                //Banner - not used as load ad button is not present
                [bannerView removeFromSuperview];
                
                [[Tapdaq sharedSession] loadBanner:TDMBannerStandard];
                break;
            default:
                break;
        }
    }else{
        switch (self.adType.selectedSegmentIndex) {
            case 0:
                //Interstitial
                [[Tapdaq sharedSession] loadDirectInterstitial:[self networkStringToEnum:self.adNetwork]];
                break;
            case 1:
                //Video
                [[Tapdaq sharedSession] loadDirectVideo:[self networkStringToEnum:self.adNetwork]];
                break;
            case 2:
                //Rewarded
                [[Tapdaq sharedSession] loadDirectRewardedVideo:[self networkStringToEnum:self.adNetwork]];
                break;
            case 3:
                //Banner
                [bannerView removeFromSuperview];
                
                [[Tapdaq sharedSession] loadDirectBanner:[self networkStringToEnum:self.adNetwork]
                                                    size:TDMBannerStandard];
                break;
            default:
                break;
        }
    }
    
}

- (IBAction)showingAd:(id)sender {
    TDMDebugLog(@"showingAd");
    
    //Check what's selected
    if ([self.adNetwork isEqualToString:@"Mediated"]){
        switch (self.adType.selectedSegmentIndex) {
            case 0:
                //Interstitial
                [[Tapdaq sharedSession] showInterstitial];
                break;
            case 1:
                //Video
                [[Tapdaq sharedSession] showVideo];
                break;
            case 2:
                //Rewarded
                [[Tapdaq sharedSession] showRewardedVideo];
                break;
            case 3:
                //Banner
                bannerView = [[Tapdaq sharedSession] showBanner];
                
                //Place it at bottom
                bannerView.frame = CGRectMake((self.view.frame.size.width-bannerView.frame.size.width)/2, self.view.frame.size.height-bannerView.frame.size.height, bannerView.frame.size.width, bannerView.frame.size.height);
                
                [self.view addSubview:bannerView];
                
                
                
                break;
            default:
                break;
        }
    }else{
        switch (self.adType.selectedSegmentIndex) {
            case 0:
                //Interstitial
                [[Tapdaq sharedSession] showDirectInterstitial:[self networkStringToEnum:self.adNetwork]];
                break;
            case 1:
                //Video
                [[Tapdaq sharedSession] showDirectVideo:[self networkStringToEnum:self.adNetwork]];
                break;
            case 2:
                //Rewarded
                [[Tapdaq sharedSession] showDirectRewardedVideo:[self networkStringToEnum:self.adNetwork]];
                break;
            case 3:
                //Banner
                bannerView = [[Tapdaq sharedSession] showDirectBanner:[self networkStringToEnum:self.adNetwork]];
                
                //Place it at bottom
                bannerView.frame = CGRectMake((self.view.frame.size.width-bannerView.frame.size.width)/2, self.view.frame.size.height-bannerView.frame.size.height, bannerView.frame.size.width, bannerView.frame.size.height);
                
                [self.view addSubview:bannerView];
                
                break;
            default:
                break;
        }
    }
}

- (IBAction)refreshAdSource:(id)sender {
    TDMDebugLog(@"refreshAdSource");
    
    self.debug.text = @"";
    
    [[Tapdaq sharedSession] refreshAdNetwork:[self networkStringToEnum:self.adNetwork]];
    
}

- (TDMNetwork)networkStringToEnum:(NSString *)network{
    TDMNetwork networkToCheck;
    
    if ([network isEqualToString:@"AdColony"]) {
        networkToCheck = TDMAdColony;
    }else if ([network isEqualToString:@"AdMob"]) {
        networkToCheck = TDMAdMob;
    }else if ([network isEqualToString:@"AppLovin"]) {
        networkToCheck = TDMApplovin;
    }else if ([network isEqualToString:@"Chartboost"]) {
        networkToCheck = TDMChartboost;
    }else if ([network isEqualToString:@"Facebook"]) {
        networkToCheck = TDMFacebookAudienceNetwork;
    }else if ([network isEqualToString:@"InMobi"]) {
        networkToCheck = TDMInMobi;
    }else if ([network isEqualToString:@"UnityAds"]) {
        networkToCheck = TDMUnityAds;
    }else if ([network isEqualToString:@"Vungle"]) {
        networkToCheck = TDMVungle;
    }else if ([network isEqualToString:@"Mediated"]) {
        networkToCheck = TDMMediatedNetwork;
    }
    
    return networkToCheck;
}

#pragma mark -
#pragma mark Debug view update

- (void)updateDebugLabel:(NSString *)debugText
{
    dispatch_async(dispatch_get_main_queue(), ^{
        if (debugLines>9) {
            self.debug.text = debugText;
            debugLines = 0;
        }else{
            self.debug.text = [self.debug.text stringByAppendingString:debugText];
        }
        debugLines++;
    });
    
}

//Use delegates to update the debug label
#pragma mark > Initialized

-(void)didLoadTapdaq{
    TDMDebugLog(@"Delegate test: didLoadTapdaq");
    [self updateDebugLabel:@"\n didLoadTapdaq"];
}

#pragma mark > Banner

- (void)didLoadBanner{
    TDMDebugLog(@"Delegate test: didLoadBanner");
    [self updateDebugLabel:@"\n didLoadBanner"];
}

- (void)willDisplayBanner{
    TDMDebugLog(@"Delegate test: willDisplayBanner");
    [self updateDebugLabel:@"\n willDisplayBanner"];
}

- (void)didDisplayBanner{
    TDMDebugLog(@"Delegate test: didDisplayBanner");
    [self updateDebugLabel:@"\n didDisplayBanner"];
}

-(void)didClickBanner{
    TDMDebugLog(@"Delegate test: didClickBanner");
    [self updateDebugLabel:@"\n didClickBanner"];
}

- (void)didFailToDisplayBanner{
    TDMDebugLog(@"Delegate test: didFailToDisplayBanner");
    [self updateDebugLabel:@"\n didFailToDisplayBanner"];
}

- (void)didFinishHandlingClickBanner{
    TDMDebugLog(@"Delegate test: didFinishHandlingClickBanner");
    [self updateDebugLabel:@"\n didFinishHandlingClickBanner"];
}

#pragma mark > Interstitial

- (void)loadInterstitial{
    TDMDebugLog(@"Delegate test: loadInterstitial");
    [self updateDebugLabel:@"\n loadInterstitial"];
}

- (void)didLoadInterstitial{
    TDMDebugLog(@"Delegate test: didLoadInterstitial");
    [self updateDebugLabel:@"\n didLoadInterstitial"];
}

- (void)didLoadInterstitialForPlacementTag:(NSString *)tag{
    
    NSString *description = [NSString stringWithFormat:@"\n didLoadInterstitialForPlacementTag %@",
                             tag];
    
    TDMDebugLog(@"Delegate test: %@", description);
    [self updateDebugLabel:description];
}

- (void)didDisplayInterstitial{
    TDMDebugLog(@"Delegate test: didDisplayInterstitial");
    [self updateDebugLabel:@"\n didDisplayInterstitial"];
}

- (void)didFailToDisplayInterstitial{
    TDMDebugLog(@"Delegate test: didFailToDisplayInterstitial");
    [self updateDebugLabel:@"\n didFailToDisplayInterstitial"];
}

- (void)didCloseInterstitial{
    TDMDebugLog(@"Delegate test: didCloseInterstitial");
    [self updateDebugLabel:@"\n didCloseInterstitial"];
}

- (void)willCloseInterstitial{
    TDMDebugLog(@"Delegate test: willCloseInterstitial");
    [self updateDebugLabel:@"\n willCloseInterstitial"];
}

- (void)didClickInterstitial{
    TDMDebugLog(@"Delegate test: didClickInterstitial");
    [self updateDebugLabel:@"\n didClickInterstitial"];
}

- (void)willDisplayInterstitial{
    TDMDebugLog(@"Delegate test: willDisplayInterstitial");
    [self updateDebugLabel:@"\n willDisplayInterstitial"];
}

#pragma mark > Video

- (void)didLoadVideo{
    TDMDebugLog(@"Delegate test: didLoadVideo");
    [self updateDebugLabel:@"\n didLoadVideo"];
}

- (void)didLoadVideoForPlacementTag:(NSString *)tag{
    
    NSString *description = [NSString stringWithFormat:@"\n didLoadVideoForPlacementTag %@",
                             tag];
    
    TDMDebugLog(@"Delegate test: %@", description);
    [self updateDebugLabel:description];
}

- (void)willDisplayVideo{
    TDMDebugLog(@"Delegate test: willDisplayVideo");
    [self updateDebugLabel:@"\n willDisplayVideo"];
}

- (void)didDisplayVideo{
    TDMDebugLog(@"Delegate test: didDisplayVideo");
    [self updateDebugLabel:@"\n didDisplayVideo"];
}

- (void)didFailToDisplayVideo{
    TDMDebugLog(@"Delegate test: didFailToDisplayVideo");
    [self updateDebugLabel:@"\n didFailToDisplayVideo"];
}

- (void)willCloseVideo{
    TDMDebugLog(@"Delegate test: willCloseVideo");
    [self updateDebugLabel:@"\n willCloseVideo"];
}

- (void)didCloseVideo{
    TDMDebugLog(@"Delegate test: didCloseVideo");
    [self updateDebugLabel:@"\n didCloseVideo"];
}

- (void)didClickVideo{
    TDMDebugLog(@"Delegate test: didClickVideo");
    [self updateDebugLabel:@"\n didClickVideo"];
}

- (void)willDisplayVideoEndAd{
    TDMDebugLog(@"Delegate test: willDisplayVideoEndAd");
    [self updateDebugLabel:@"\n willDisplayVideoEndAd"];
}

#pragma mark > Rewarded Video

- (void)didLoadRewardedVideo{
    TDMDebugLog(@"Delegate test: didLoadRewardedVideo");
    [self updateDebugLabel:@"\n didLoadRewardedVideo"];
}

- (void)didLoadRewardedVideoForPlacementTag:(NSString *)tag{
    
    NSString *description = [NSString stringWithFormat:@"\n didLoadRewardedVideoForPlacementTag %@",
                             tag];
    
    TDMDebugLog(@"Delegate test: %@", description);
    [self updateDebugLabel:description];
}

- (void)rewardValidationSuceeded:(NSString *) rewardName
                    rewardAmount:(int) rewardAmount{
    TDMDebugLog(@"Delegate test: rewardValidationSuceeded");
    [self updateDebugLabel:[NSString stringWithFormat:@"\n rewardValidationSuceeded:- Reward name:%@ Amount:%d", rewardName, rewardAmount]];
}

- (void)rewardValidationExceededQuota{
    TDMDebugLog(@"Delegate test: rewardValidationExceededQuota");
    [self updateDebugLabel:@"\n rewardValidationExceededQuota"];
}

- (void)rewardValidationRejected{
    TDMDebugLog(@"Delegate test: rewardValidationRejected");
    [self updateDebugLabel:@"\n rewardValidationRejected"];
}

- (void)rewardValidationErrored{
    TDMDebugLog(@"Delegate test: rewardValidationErrored");
    [self updateDebugLabel:@"\n rewardValidationErrored"];
}

- (void)userDeclinedToViewRewardedVideo{
    TDMDebugLog(@"Delegate test: userDeclinedToViewRewardedVideo");
    [self updateDebugLabel:@"\n userDeclinedToViewRewardedVideo"];
}

/*-(BOOL)prefersStatusBarHidden{
 return YES;
 }*/

/*
 #pragma mark - Navigation
 
 // In a storyboard-based application, you will often want to do a little preparation before navigation
 - (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
 // Get the new view controller using [segue destinationViewController].
 // Pass the selected object to the new view controller.
 }
 */

@end
