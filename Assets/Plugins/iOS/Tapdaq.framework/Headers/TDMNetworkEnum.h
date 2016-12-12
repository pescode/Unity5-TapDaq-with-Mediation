//
//  TDMNetworkEnum.h
//  Tapdaq iOS SDK
//
//  Created by Mukund Agarwal on 13/10/2016.
//  Copyright © 2016 Tapdaq. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef NS_ENUM(NSUInteger, TDMNetwork) {
    
    TDMTapdaq,
    TDMAdColony,
    TDMAdMob,
    TDMApplovin,
    TDMChartboost,
    TDMInMobi,
    TDMFacebookAudienceNetwork,
    TDMVungle,
    TDMUnityAds,
    TDMMediatedNetwork,
    TDMNone
};

#define kTDMNetwork @"Tapdaq", @"AdColony", @"AdMob", @"AppLovin", @"Chartboost", @"InMobi", @"Facebook Audience Network", @"Vungle", @"UnityAds", @"Mediated network", @"None", nil
