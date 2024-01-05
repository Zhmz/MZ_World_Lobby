//
//  QuickpassModel.h
//  UnityFramework
//
//  Created by 罗礼豪 on 2021/9/10.
//

#import <Foundation/Foundation.h>
#import <NTESQuickPass/NTESQuickPass.h>

typedef void (*ConfigHandler) (const char * _Nullable object);

NS_ASSUME_NONNULL_BEGIN

@interface QuickpassModel : NSObject {
    ConfigHandler configHandler;
}

- (NTESQuickLoginModel *)setupModel:(NSDictionary *)dict config:(ConfigHandler)config;

@end

NS_ASSUME_NONNULL_END



