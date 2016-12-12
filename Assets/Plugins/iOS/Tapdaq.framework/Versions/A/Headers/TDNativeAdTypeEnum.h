//
//  TDNativeAdTypeEnum.h
//  Tapdaq
//
//  Created by Tapdaq <support@tapdaq.com>
//  Copyright (c) 2016 Tapdaq. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef NS_ENUM(NSUInteger, TDNativeAdType) {
    
    TDNativeAdType1x1Large,
    TDNativeAdType1x1Medium,
    TDNativeAdType1x1Small,
    
    TDNativeAdType1x2Large,
    TDNativeAdType1x2Medium,
    TDNativeAdType1x2Small,
    
    TDNativeAdType2x1Large,
    TDNativeAdType2x1Medium,
    TDNativeAdType2x1Small,
    
    TDNativeAdType2x3Large,
    TDNativeAdType2x3Medium,
    TDNativeAdType2x3Small,
    
    TDNativeAdType3x2Large,
    TDNativeAdType3x2Medium,
    TDNativeAdType3x2Small,
    
    TDNativeAdType1x5Large,
    TDNativeAdType1x5Medium,
    TDNativeAdType1x5Small,
    
    TDNativeAdType5x1Large,
    TDNativeAdType5x1Medium,
    TDNativeAdType5x1Small
    
};

#pragma mark v2

//#define kTDNativeAdType @"SQUARE_LARGE", @"SQUARE_MEDIUM", @"SQUARE_SMALL", @"NEWSFEED_TALL_LARGE", @"NEWSFEED_TALL_MEDIUM", @"NEWSFEED_TALL_SMALL", @"NEWSFEED_WIDE_LARGE", @"NEWSFEED_WIDE_MEDIUM", @"NEWSFEED_WIDE_SMALL", @"FULLSCREEN_TALL_LARGE", @"FULLSCREEN_TALL_MEDIUM", @"FULLSCREEN_TALL_SMALL", @"FULLSCREEN_WIDE_LARGE", @"FULLSCREEN_WIDE_MEDIUM", @"FULLSCREEN_WIDE_SMALL", @"STRIP_TALL_LARGE", @"STRIP_TALL_MEDIUM", @"STRIP_TALL_SMALL", @"STRIP_WIDE_LARGE", @"STRIP_WIDE_MEDIUM", @"STRIP_WIDE_SMALL", nil

#pragma mark v3

#define kTDNativeAdType @"square_large", @"square_medium", @"square_small", @"newsfeed_tall_large", @"newsfeed_tall_medium", @"newsfeed_tall_small", @"newsfeed_wide_large", @"newsfeed_wide_medium", @"newsfeed_wide_small", @"fullscreen_tall_large", @"fullscreen_tall_medium", @"fullscreen_tall_small", @"fullscreen_wide_large", @"fullscreen_wide_medium", @"fullscreen_wide_small", @"strip_tall_large", @"strip_tall_medium", @"strip_tall_small", @"strip_wide_large", @"strip_wide_medium", @"strip_wide_small", nil
