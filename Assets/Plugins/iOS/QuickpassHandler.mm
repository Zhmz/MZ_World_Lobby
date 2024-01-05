//
//  QuickpassHandler.m
//  Unity-iPhone
//
//  Created by 罗礼豪 on 2021/9/10.
//

#import "QuickpassHandler.h"
#import <NTESQuickPass/NTESQuickPass.h>
#import "QuickpassModel.h"

@implementation QuickpassHandler

typedef void (*preFetchNumberHandler) (const char *object);
typedef void (*OnPassLoginHandler) (const char *object);
typedef void (*ConfigHandler) (const char *object);

extern "C" void init(char *option,int timeout) {
    NSString *optionString = [[NSString alloc] initWithUTF8String:option];
    if (optionString && optionString.length > 0) {
        [[NTESQuickLoginManager sharedInstance] registerWithBusinessID:optionString];
        NSLog(@"初始化成功");
    } else {
        NSLog(@"业务ID输入有误");
    }
    if (timeout > 0) {
        [[NTESQuickLoginManager sharedInstance] setTimeoutInterval:timeout];
    }
}

extern "C" void preFetchNumber(preFetchNumberHandler resultHandler) {
    [[NTESQuickLoginManager sharedInstance] getPhoneNumberCompletion:^(NSDictionary * _Nonnull resultDic) {
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:resultDic options:NSJSONWritingPrettyPrinted error:nil];
        NSString *jsonStirng = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        resultHandler([jsonStirng UTF8String]);
    }];

}

extern "C" void setupUiConfig(char *option,ConfigHandler configHandler) {
   
        NSString *optionString = [[NSString alloc] initWithUTF8String:option];
        if (optionString && optionString.length > 0) {
            NSData *jsonData = [optionString dataUsingEncoding:NSUTF8StringEncoding];
            NSDictionary *options = [NSJSONSerialization JSONObjectWithData:jsonData
                                                            options:NSJSONReadingMutableContainers
                                                              error:nil];
            NSDictionary *dict = options;
            if ([dict isKindOfClass:[NSDictionary class]]) {
                dispatch_async(dispatch_get_main_queue(), ^{
                    NTESQuickLoginModel *model = [[[QuickpassModel alloc] init] setupModel:dict config:configHandler];
                    [[NTESQuickLoginManager sharedInstance] setupModel:model];
                });
            }
        }
}
  

extern "C" void OnPassLogin(BOOL animated, OnPassLoginHandler resultHandler)  {
    dispatch_async(dispatch_get_main_queue(), ^{
        [[NTESQuickLoginManager sharedInstance] CUCMCTAuthorizeLoginCompletion:^(NSDictionary * _Nonnull resultDic) {
            NSData *jsonData = [NSJSONSerialization dataWithJSONObject:resultDic options:NSJSONWritingPrettyPrinted error:nil];
            NSString *jsonStirng = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            resultHandler([jsonStirng UTF8String]);
        } animated:animated];
    });
}

extern "C" void closeAuthController() {
    [[NTESQuickLoginManager sharedInstance] closeAuthController:nil];
}

extern "C" BOOL checkVerifyEnable() {
    return [[NTESQuickLoginManager sharedInstance] shouldQuickLogin];
}

@end




